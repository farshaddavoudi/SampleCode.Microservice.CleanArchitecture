using Microsoft.Extensions.Caching.Distributed;
using SampleMicroserviceApp.Identity.Application.Common.Contracts;
using SampleMicroserviceApp.Identity.Application.CQRS.Claim.Specifications;
using SampleMicroserviceApp.Identity.Domain.Constants;
using SampleMicroserviceApp.Identity.Domain.Entities.Claim;
using System.Text.Json;

namespace SampleMicroserviceApp.Identity.Infrastructure.Caching;

public class RoleClaimsCacheService(IDistributedCache distributedCache, IRepository<ClaimEntity> claimRepository) : IRoleClaimsCacheService
{
    public async Task SetRoleClaimsAsync(string roleKey, List<string> claimKeys, CancellationToken cancellationToken)
    {
        await distributedCache.SetStringAsync(CacheKeyConst.RoleClaims(roleKey!),
            JsonSerializer.Serialize(claimKeys),
            cancellationToken);
    }

    public async Task<List<string>> GetRoleClaimsAsync(string roleKey, CancellationToken cancellationToken)
    {
        List<string> claims = new();

        var claimsSerialized = await distributedCache.GetStringAsync(CacheKeyConst.RoleClaims(roleKey), cancellationToken);

        if (string.IsNullOrWhiteSpace(claimsSerialized))
        {
            var roleClaimsFromDb = await claimRepository.ToListDistinctAsync(
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