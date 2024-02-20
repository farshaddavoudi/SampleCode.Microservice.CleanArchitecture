using SampleMicroserviceApp.Identity.Application.Common.Contracts;
using SampleMicroserviceApp.Identity.Application.CQRS.Role.Specifications;
using SampleMicroserviceApp.Identity.Domain.Constants;
using SampleMicroserviceApp.Identity.Domain.Entities.Role;
using StackExchange.Redis;
using System.Text.Json;

namespace SampleMicroserviceApp.Identity.Infrastructure.Caching;

public class UserRolesCacheService(
    IConnectionMultiplexer connectionMultiplexer,
    IRepository<RoleEntity> roleRepository,
    IApplicationCacheService applicationCacheService
    ) : IUserRolesCacheService
{
    private readonly IDatabase _redisDb = connectionMultiplexer.GetDatabase();

    public async Task SetUserRolesAsync(int userId, string appKey, List<string> roleKeys, CancellationToken cancellationToken)
    {
        await _redisDb.HashSetAsync(CacheKeyConst.UserRolesHash.UserRolesKey(userId),
            CacheKeyConst.UserRolesHash.UserRolesHashField(appKey),
            JsonSerializer.Serialize(roleKeys));
    }

    public async Task<List<string>> GetUserRolesAsync(int userId, List<string> appKeys, CancellationToken cancellationToken)
    {
        List<string> roles = new();

        foreach (var appKey in appKeys)
        {
            var rolesSerialized = await _redisDb.HashGetAsync(CacheKeyConst.UserRolesHash.UserRolesKey(userId),
                CacheKeyConst.UserRolesHash.UserRolesHashField(appKey));

            if (string.IsNullOrWhiteSpace(rolesSerialized))
            {
                var app = await applicationCacheService.GetAppAsync(appKey, cancellationToken);

                if (app is null)
                {
                    continue;
                }

                var userRolesFromDb = await roleRepository.ToListDistinctAsync(
                    new UserRoleKeysInAppByAppIdAndUserId(app.Value.MainApp.Id, userId), cancellationToken);

                if (userRolesFromDb.Any())
                {
                    roles.AddRange(userRolesFromDb!);

                    await SetUserRolesAsync(userId, appKey, userRolesFromDb!, cancellationToken);
                }
            }
            else
            {
                roles.AddRange(JsonSerializer.Deserialize<List<string>>(rolesSerialized)!);
            }
        }

        return roles;
    }
}