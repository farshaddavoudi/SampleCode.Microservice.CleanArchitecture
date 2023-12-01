using SampleMicroserviceApp.Identity.Application.Common.Contracts;
using SampleMicroserviceApp.Identity.Application.CQRS.Role.Commands.ChangeRoleUsers.Events;
using SampleMicroserviceApp.Identity.Application.CQRS.Role.Specifications;
using SampleMicroserviceApp.Identity.Domain.Entities.User;

namespace SampleMicroserviceApp.Identity.Application.CQRS.Role.Commands.ChangeRoleUsers;

public record ChangeRoleUsersCommand(int RoleId, List<int> UserIds) : IRequest;

// Handler
public class ChangeRoleUsersCommandHandler : IRequestHandler<ChangeRoleUsersCommand>
{
    private readonly IRepository<UserRoleEntity> _userRoleRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMediator _mediator;

    #region ctor

    public ChangeRoleUsersCommandHandler(IRepository<UserRoleEntity> userRoleRepository, IUnitOfWork unitOfWork, IMediator mediator)
    {
        _userRoleRepository = userRoleRepository;
        _unitOfWork = unitOfWork;
        _mediator = mediator;
    }

    #endregion

    public async Task Handle(ChangeRoleUsersCommand request, CancellationToken cancellationToken)
    {
        var allPriorRoleUsers = await _userRoleRepository.ToListAsync(
                new UserRolePairsByRoleIdSpec(request.RoleId), cancellationToken);

        foreach (var requestUserId in request.UserIds)
        {
            if (allPriorRoleUsers.Any(x => x.Id == requestUserId))
                continue; //Role already contains this User

            var newUserForRole = new UserRoleEntity { RoleId = request.RoleId, UserId = requestUserId };

            await _userRoleRepository.AddAsync(newUserForRole, cancellationToken, false);
        }

        foreach (var priorUser in allPriorRoleUsers)
        {
            if (request.UserIds.Any(cId => cId == priorUser.UserId) is false)
            {
                await _userRoleRepository.DeleteAsync(priorUser, cancellationToken, false);
            }
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var affectedUserIds = new List<int>();
        affectedUserIds.AddRange(request.UserIds);
        affectedUserIds.AddRange(allPriorRoleUsers.Select(x => x.UserId));
        affectedUserIds = affectedUserIds.Distinct().ToList();

        await _mediator.Publish(new RoleUsersChangedEvent(request.RoleId, affectedUserIds), cancellationToken);
    }
}