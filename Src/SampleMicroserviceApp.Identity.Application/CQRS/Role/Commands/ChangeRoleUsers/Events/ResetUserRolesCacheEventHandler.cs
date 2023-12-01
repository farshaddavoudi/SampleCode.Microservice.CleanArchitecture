using SampleMicroserviceApp.Identity.Application.Common.Contracts;
using SampleMicroserviceApp.Identity.Application.CQRS.Role.Specifications;
using SampleMicroserviceApp.Identity.Domain.Entities.Role;

namespace SampleMicroserviceApp.Identity.Application.CQRS.Role.Commands.ChangeRoleUsers.Events;

public class ResetUserRolesCacheEventHandler : INotificationHandler<RoleUsersChangedEvent>
{
    private readonly IUserRolesCacheService _userRolesCacheService;
    private readonly IRepository<RoleEntity> _roleRepository;

    #region ctor

    public ResetUserRolesCacheEventHandler(IRepository<RoleEntity> roleRepository, IUserRolesCacheService userRolesCacheService)
    {
        _roleRepository = roleRepository;
        _userRolesCacheService = userRolesCacheService;
    }

    #endregion

    public async Task Handle(RoleUsersChangedEvent notification, CancellationToken cancellationToken)
    {
        var app = await _roleRepository.FirstOrDefaultProjectedAsync(
            new ApplicationIdAndKeyByRoleIdSpec(notification.RoleId), cancellationToken);

        var appId = app!.Item1;
        var appKey = app.Item2;

        foreach (var userId in notification.AffectedUserIds)
        {
            var roleKeys = await _roleRepository.ToListProjectedAsync(
                new UserRoleKeysInAppByAppIdAndUserId(appId, userId), cancellationToken);

            if (roleKeys.Any())
            {
                await _userRolesCacheService.SetUserRolesAsync(userId, appKey, roleKeys, cancellationToken);
            }
        }
    }
}