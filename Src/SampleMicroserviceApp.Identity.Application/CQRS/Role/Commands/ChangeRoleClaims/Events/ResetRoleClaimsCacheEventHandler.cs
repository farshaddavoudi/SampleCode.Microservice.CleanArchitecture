using SampleMicroserviceApp.Identity.Application.Common.Contracts;
using SampleMicroserviceApp.Identity.Application.CQRS.Claim.Specifications;
using SampleMicroserviceApp.Identity.Application.CQRS.Role.Specifications;
using SampleMicroserviceApp.Identity.Domain.Entities.Claim;
using SampleMicroserviceApp.Identity.Domain.Entities.Role;

namespace SampleMicroserviceApp.Identity.Application.CQRS.Role.Commands.ChangeRoleClaims.Events;

public class ResetRoleClaimsCacheEventHandler : INotificationHandler<RoleClaimsChangedEvent>
{
    private readonly IRoleClaimsCacheService _roleClaimsCacheService;
    private readonly IRepository<ClaimEntity> _claimRepository;
    private readonly IRepository<RoleEntity> _roleRepository;

    #region ctor

    public ResetRoleClaimsCacheEventHandler(IRoleClaimsCacheService roleClaimsCacheService, IRepository<ClaimEntity> claimRepository, IRepository<RoleEntity> roleRepository)
    {
        _roleClaimsCacheService = roleClaimsCacheService;
        _claimRepository = claimRepository;
        _roleRepository = roleRepository;
    }

    #endregion

    public async Task Handle(RoleClaimsChangedEvent notification, CancellationToken cancellationToken)
    {
        var roleKey = await _roleRepository.FirstOrDefaultProjectedAsync(
            new RoleKeyById(notification.RoleId), cancellationToken);

        var claimKeys = await _claimRepository.ToListProjectedAsync(
                new ClaimKeyWithinIdsSpec(notification.ClaimIds), cancellationToken);

        if (claimKeys.Any())
            await _roleClaimsCacheService.SetRoleClaimsAsync(roleKey!, claimKeys!, cancellationToken);
    }
}