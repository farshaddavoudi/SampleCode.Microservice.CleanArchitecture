using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;
using SampleMicroserviceApp.Identity.Application.Common.Contracts;
using SampleMicroserviceApp.Identity.Application.CQRS.Claim.Specifications;
using SampleMicroserviceApp.Identity.Domain.Constants;
using SampleMicroserviceApp.Identity.Domain.Entities.Claim;

namespace SampleMicroserviceApp.Identity.Infrastructure.Caching;

public class RoleClaimsCacheService : IRoleClaimsCacheService
{
    private readonly IDistributedCache _distributedCache;
    private readonly IRepository<ClaimEntity> _claimRepository;

    #region ctor

    public RoleClaimsCacheService(IDistributedCache distributedCache, IRepository<ClaimEntity> claimRepository)
    {
        _distributedCache = distributedCache;
        _claimRepository = claimRepository;
    }

    #endregion

    public async Task SetRoleClaimsAsync(string roleKey, List<string> claimKeys, CancellationToken cancellationToken)
    {
        await _distributedCache.SetStringAsync(CacheKeyConst.RoleClaims(roleKey!),
            JsonSerializer.Serialize(claimKeys),
            cancellationToken);
    }

    public async Task<List<string>> GetRoleClaimsAsync(string roleKey, CancellationToken cancellationToken)
    {
        List<string> claims = new();

        var claimsSerialized = await _distributedCache.GetStringAsync(CacheKeyConst.RoleClaims(roleKey), cancellationToken);

        if (string.IsNullOrWhiteSpace(claimsSerialized))
        {
            var roleClaimsFromDb = await _claimRepository.ToListProjectedDistinctAsync(
                new ClaimKeyByRoleKeySpec(roleKey), cancellationToken);

            if (roleClaimsFromDb.Any())
            {
                claims.AddRange(roleClaimsFromDb!);

                await SetRoleClaimsAsync(roleKey, roleClaimsFromDb!, cancellationToken);
            }
        }
        else
        {
            claims.AddRange(JsonSerializer.Deserialize<List<string>>(claimsSerialized)!);
        }

        return claims;
    }
}