using AutoMapper;
using Microsoft.Extensions.Caching.Distributed;
using SampleMicroserviceApp.Identity.Application.Common.Contracts;
using SampleMicroserviceApp.Identity.Application.CQRS.User.DTOs;
using SampleMicroserviceApp.Identity.Application.CQRS.User.Specifications;
using SampleMicroserviceApp.Identity.Domain.ConfigurationSettings;
using SampleMicroserviceApp.Identity.Domain.Constants;
using SampleMicroserviceApp.Identity.Domain.Entities.User;
using SampleMicroserviceApp.Identity.Domain.Extensions;
using System.Text.Json;

namespace SampleMicroserviceApp.Identity.Infrastructure.Caching;

public class UserCacheService(
    IDistributedCache distributedCache,
    IRepository<UserEntity> userRepository,
    IMapper mapper,
    AppSettings appSettings
    ) : IUserCacheService
{
    public async Task SetUserAsync(UserMiniDto user, CancellationToken cancellationToken)
    {
        await distributedCache.SetStringAsync(CacheKeyConst.User(user.Id),
            JsonSerializer.Serialize(user),
            cancellationToken);
    }

    public async Task<UserMiniDto?> GetUserAsync(int userId, CancellationToken cancellationToken)
    {
        var userSerialized = await distributedCache.GetStringAsync(CacheKeyConst.User(userId), cancellationToken);

        UserMiniDto? user;

        if (string.IsNullOrWhiteSpace(userSerialized))
        {
            user = await userRepository.FirstOrDefaultAsync<UserMiniDto>(
                new UserByIdSpec(userId), cancellationToken);

            if (user is not null)
            {
                await SetUserAsync(user, cancellationToken);
            }
        }
        else
        {
            user = JsonSerializer.Deserialize<UserMiniDto>(userSerialized);
        }

        return user;
    }

    public async Task SetRefreshToken(int userId, string refreshToken, CancellationToken cancellationToken)
    {
        // Save both user as key and RefreshToken as key so we can query in reverse order

        DistributedCacheEntryOptions options = new() { AbsoluteExpirationRelativeToNow = appSettings.AuthSettings!.RefreshTokenTtl };
        await distributedCache.SetStringAsync(CacheKeyConst.UserRefreshToken(userId), refreshToken, options, cancellationToken);
        await distributedCache.SetStringAsync(CacheKeyConst.UserRefreshToken(refreshToken), userId.ToString(), options, cancellationToken);
    }

    public async Task RemoveRefreshToken(int userId, CancellationToken cancellationToken)
    {
        var refreshToken = await GetRefreshTokenByUserId(userId, cancellationToken);

        if (refreshToken.IsNotNullOrWhitespace())
        {
            await distributedCache.RemoveAsync(CacheKeyConst.UserRefreshToken(refreshToken!), cancellationToken);
        }

        await distributedCache.RemoveAsync(CacheKeyConst.UserRefreshToken(userId), cancellationToken);
    }

    public async Task<int?> GetUserIdByRefreshToken(string refreshToken, CancellationToken cancellationToken)
    {
        var userIdStr = await distributedCache.GetStringAsync(CacheKeyConst.UserRefreshToken(refreshToken), cancellationToken);

        return userIdStr?.ToInt(true);
    }

    public async Task<string?> GetRefreshTokenByUserId(int userId, CancellationToken cancellationToken)
    {
        return await distributedCache.GetStringAsync(CacheKeyConst.UserRefreshToken(userId), cancellationToken);
    }
}