using SampleMicroserviceApp.Identity.Application.Common.Contracts;
using SampleMicroserviceApp.Identity.Application.CQRS.Role.Commands.ChangeRoleClaims.Events;
using SampleMicroserviceApp.Identity.Application.CQRS.Role.Specifications;
using SampleMicroserviceApp.Identity.Domain.Entities.Role;

namespace SampleMicroserviceApp.Identity.Application.CQRS.Role.Commands.ChangeRoleClaims;

public record ChangeRoleClaimsCommand(int RoleId, List<int> ClaimIds) : IRequest;

// Handler
public class ReplaceRoleClaimsCommandHandler(
    IRepository<RoleClaimEntity> roleClaimRepository,
    IUnitOfWork unitOfWork,
    IMediator mediator
    ) : IRequestHandler<ChangeRoleClaimsCommand>
{
    public async Task Handle(ChangeRoleClaimsCommand request, CancellationToken cancellationToken)
    {
        // TODO: Check the claims and the role all belong to the same application

        var allPriorRoleClaims = await roleClaimRepository.ToListAsync(
                new RoleClaimPairsByRoleIdSpec(request.RoleId), cancellationToken);

        foreach (var requestClaimId in request.ClaimIds)
        {
            if (allPriorRoleClaims.Any(x => x.Id == requestClaimId))
                continue; //Role already has this claim

            var newClaimForRole = new RoleClaimEntity { RoleId = request.RoleId, ClaimId = requestClaimId };

            await roleClaimRepository.AddAsync(newClaimForRole, cancellationToken, false);
        }

        foreach (var priorClaim in allPriorRoleClaims)
        {
            if (request.ClaimIds.Any(cId => cId == priorClaim.ClaimId) is false)
            {
                await roleClaimRepository.DeleteAsync(priorClaim, cancellationToken, false);
            }
        }

        await unitOfWork.SaveChangesAsync(cancellationToken);

        await mediator.Publish(new RoleClaimsChangedEvent(request.RoleId, request.ClaimIds), cancellationToken);
    }
}