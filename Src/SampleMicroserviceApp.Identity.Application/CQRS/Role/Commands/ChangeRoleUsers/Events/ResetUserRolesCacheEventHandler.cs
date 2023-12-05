using SampleMicroserviceApp.Identity.Application.Common.Contracts;
using SampleMicroserviceApp.Identity.Application.CQRS.Role.Specifications;
using SampleMicroserviceApp.Identity.Domain.Entities.Role;

namespace SampleMicroserviceApp.Identity.Application.CQRS.Role.Commands.ChangeRoleUsers.Events;

public class ResetUserRolesCacheEventHandler(IUserRolesCacheService userRolesCacheService, IRepository<RoleEntity> roleRepository)
    : INotificationHandler<RoleUsersChangedEvent>
{
    public async Task Handle(RoleUsersChangedEvent notification, CancellationToken cancellationToken)
    {
        var app = await roleRepository.FirstOrDefaultProjectedAsync(
            new ApplicationIdAndKeyByRoleIdSpec(notification.RoleId), cancellationToken);

        var appId = app!.Item1;
        var appKey = app.Item2;

        foreach (var userId in notification.AffectedUserIds)
        {
            var roleKeys = await roleRepository.ToListProjectedAsync(
                new UserRoleKeysInAppByAppIdAndUserId(appId, userId), cancellationToken);

            if (roleKeys.Any())
            {
                await userRolesCacheService.SetUserRolesAsync(userId, appKey, roleKeys, cancellationToken);
            }
        }
    }
}