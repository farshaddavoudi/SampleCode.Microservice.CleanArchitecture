using SampleMicroserviceApp.Identity.Application.Common.Contracts;
using SampleMicroserviceApp.Identity.Application.CQRS.Role.Commands.ChangeRoleClaims.Events;
using SampleMicroserviceApp.Identity.Application.CQRS.Role.Specifications;
using SampleMicroserviceApp.Identity.Domain.Entities.Role;

namespace SampleMicroserviceApp.Identity.Application.CQRS.Role.Commands.ChangeRoleClaims;

public record ChangeRoleClaimsCommand(int RoleId, List<int> ClaimIds) : IRequest;

// Handler
public class ReplaceRoleClaimsCommandHandler : IRequestHandler<ChangeRoleClaimsCommand>
{
    private readonly IRepository<RoleClaimEntity> _roleClaimRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMediator _mediator;

    #region ctor

    public ReplaceRoleClaimsCommandHandler(IRepository<RoleClaimEntity> roleClaimRepository, IUnitOfWork unitOfWork, IMediator mediator)
    {
        _roleClaimRepository = roleClaimRepository;
        _unitOfWork = unitOfWork;
        _mediator = mediator;
    }

    #endregion

    public async Task Handle(ChangeRoleClaimsCommand request, CancellationToken cancellationToken)
    {
        // TODO: Check the claims and the role all belong to the same application

        var allPriorRoleClaims = await _roleClaimRepository.ToListAsync(
                new RoleClaimPairsByRoleIdSpec(request.RoleId), cancellationToken);

        foreach (var requestClaimId in request.ClaimIds)
        {
            if (allPriorRoleClaims.Any(x => x.Id == requestClaimId))
                continue; //Role already has this claim

            var newClaimForRole = new RoleClaimEntity { RoleId = request.RoleId, ClaimId = requestClaimId };

            await _roleClaimRepository.AddAsync(newClaimForRole, cancellationToken, false);
        }

        foreach (var priorClaim in allPriorRoleClaims)
        {
            if (request.ClaimIds.Any(cId => cId == priorClaim.ClaimId) is false)
            {
                await _roleClaimRepository.DeleteAsync(priorClaim, cancellationToken, false);
            }
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        await _mediator.Publish(new RoleClaimsChangedEvent(request.RoleId, request.ClaimIds), cancellationToken);
    }
}