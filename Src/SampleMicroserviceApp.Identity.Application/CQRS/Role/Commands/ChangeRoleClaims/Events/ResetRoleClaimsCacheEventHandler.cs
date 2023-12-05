using SampleMicroserviceApp.Identity.Application.Common.Contracts;
using SampleMicroserviceApp.Identity.Application.CQRS.Claim.Specifications;
using SampleMicroserviceApp.Identity.Application.CQRS.Role.Specifications;
using SampleMicroserviceApp.Identity.Domain.Entities.Claim;
using SampleMicroserviceApp.Identity.Domain.Entities.Role;

namespace SampleMicroserviceApp.Identity.Application.CQRS.Role.Commands.ChangeRoleClaims.Events;

public class ResetRoleClaimsCacheEventHandler(
        IRoleClaimsCacheService roleClaimsCacheService,
        IRepository<ClaimEntity> claimRepository,
        IRepository<RoleEntity> roleRepository
        )
    : INotificationHandler<RoleClaimsChangedEvent>
{
    public async Task Handle(RoleClaimsChangedEvent notification, CancellationToken cancellationToken)
    {
        var roleKey = await roleRepository.FirstOrDefaultProjectedAsync(
            new RoleKeyById(notification.RoleId), cancellationToken);

        var claimKeys = await claimRepository.ToListProjectedAsync(
                new ClaimKeyWithinIdsSpec(notification.ClaimIds), cancellationToken);

        if (claimKeys.Any())
            await roleClaimsCacheService.SetRoleClaimsAsync(roleKey!, claimKeys!, cancellationToken);
    }
}