using AutoMapper;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;
using SampleMicroserviceApp.Identity.Application.Common.Contracts;
using SampleMicroserviceApp.Identity.Application.CQRS.User.DTOs;
using SampleMicroserviceApp.Identity.Application.CQRS.User.Specifications;
using SampleMicroserviceApp.Identity.Domain.ConfigurationSettings;
using SampleMicroserviceApp.Identity.Domain.Constants;
using SampleMicroserviceApp.Identity.Domain.Entities.User;
using SampleMicroserviceApp.Identity.Domain.Extensions;

namespace SampleMicroserviceApp.Identity.Infrastructure.Caching;

public class UserCacheService : IUserCacheService
{
    private readonly IDistributedCache _distributedCache;
    private readonly IRepository<UserEntity> _userRepository;
    private readonly IMapper _mapper;
    private readonly AppSettings _appSettings;

    #region ctor

    public UserCacheService(IDistributedCache distributedCache, IRepository<UserEntity> userRepository, IMapper mapper, AppSettings appSettings)
    {
        _distributedCache = distributedCache;
        _userRepository = userRepository;
        _mapper = mapper;
        _appSettings = appSettings;
    }

    #endregion

    public async Task SetUserAsync(UserMiniDto user, CancellationToken cancellationToken)
    {
        await _distributedCache.SetStringAsync(CacheKeyConst.User(user.Id),
            JsonSerializer.Serialize(user),
            cancellationToken);
    }

    public async Task<UserMiniDto?> GetUserAsync(int userId, CancellationToken cancellationToken)
    {
        var userSerialized = await _distributedCache.GetStringAsync(CacheKeyConst.User(userId), cancellationToken);

        UserMiniDto? user;

        if (string.IsNullOrWhiteSpace(userSerialized))
        {
            user = await _userRepository.FirstOrDefaultProjectedAsync<UserMiniDto>(
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

        DistributedCacheEntryOptions options = new() { AbsoluteExpirationRelativeToNow = _appSettings.AuthSettings!.RefreshTokenTtl };
        await _distributedCache.SetStringAsync(CacheKeyConst.UserRefreshToken(userId), refreshToken, options, cancellationToken);
        await _distributedCache.SetStringAsync(CacheKeyConst.UserRefreshToken(refreshToken), userId.ToString(), options, cancellationToken);
    }

    public async Task RemoveRefreshToken(int userId, CancellationToken cancellationToken)
    {
        var refreshToken = await GetRefreshTokenByUserId(userId, cancellationToken);

        if (refreshToken.IsNotNullOrWhitespace())
        {
            await _distributedCache.RemoveAsync(CacheKeyConst.UserRefreshToken(refreshToken!), cancellationToken);
        }

        await _distributedCache.RemoveAsync(CacheKeyConst.UserRefreshToken(userId), cancellationToken);
    }

    public async Task<int?> GetUserIdByRefreshToken(string refreshToken, CancellationToken cancellationToken)
    {
        var userIdStr = await _distributedCache.GetStringAsync(CacheKeyConst.UserRefreshToken(refreshToken), cancellationToken);

        return userIdStr?.ToInt(true);
    }

    public async Task<string?> GetRefreshTokenByUserId(int userId, CancellationToken cancellationToken)
    {
        return await _distributedCache.GetStringAsync(CacheKeyConst.UserRefreshToken(userId), cancellationToken);
    }
}