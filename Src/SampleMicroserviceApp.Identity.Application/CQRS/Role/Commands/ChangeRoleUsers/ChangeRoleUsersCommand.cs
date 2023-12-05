using SampleMicroserviceApp.Identity.Application.Common.Contracts;
using SampleMicroserviceApp.Identity.Application.CQRS.Role.Commands.ChangeRoleUsers.Events;
using SampleMicroserviceApp.Identity.Application.CQRS.Role.Specifications;
using SampleMicroserviceApp.Identity.Domain.Entities.User;

namespace SampleMicroserviceApp.Identity.Application.CQRS.Role.Commands.ChangeRoleUsers;

public record ChangeRoleUsersCommand(int RoleId, List<int> UserIds) : IRequest;

// Handler
public class ChangeRoleUsersCommandHandler(
    IRepository<UserRoleEntity> userRoleRepository,
    IUnitOfWork unitOfWork,
    IMediator mediator
    ) : IRequestHandler<ChangeRoleUsersCommand>
{
    public async Task Handle(ChangeRoleUsersCommand request, CancellationToken cancellationToken)
    {
        var allPriorRoleUsers = await userRoleRepository.ToListAsync(
                new UserRolePairsByRoleIdSpec(request.RoleId), cancellationToken);

        foreach (var requestUserId in request.UserIds)
        {
            if (allPriorRoleUsers.Any(x => x.Id == requestUserId))
                continue; //Role already contains this User

            var newUserForRole = new UserRoleEntity { RoleId = request.RoleId, UserId = requestUserId };

            await userRoleRepository.AddAsync(newUserForRole, cancellationToken, false);
        }

        foreach (var priorUser in allPriorRoleUsers)
        {
            if (request.UserIds.Any(cId => cId == priorUser.UserId) is false)
            {
                await userRoleRepository.DeleteAsync(priorUser, cancellationToken, false);
            }
        }

        await unitOfWork.SaveChangesAsync(cancellationToken);

        var affectedUserIds = new List<int>();
        affectedUserIds.AddRange(request.UserIds);
        affectedUserIds.AddRange(allPriorRoleUsers.Select(x => x.UserId));
        affectedUserIds = affectedUserIds.Distinct().ToList();

        await mediator.Publish(new RoleUsersChangedEvent(request.RoleId, affectedUserIds), cancellationToken);
    }
}