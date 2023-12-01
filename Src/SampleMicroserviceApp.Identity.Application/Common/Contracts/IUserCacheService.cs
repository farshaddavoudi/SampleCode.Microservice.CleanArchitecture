using SampleMicroserviceApp.Identity.Application.CQRS.User.DTOs;

namespace SampleMicroserviceApp.Identity.Application.Common.Contracts;

public interface IUserCacheService
{
    Task SetUserAsync(UserMiniDto user, CancellationToken cancellationToken);

    Task<UserMiniDto?> GetUserAsync(int userId, CancellationToken cancellationToken);

    Task SetRefreshToken(int userId, string refreshToken, CancellationToken cancellationToken);

    Task RemoveRefreshToken(int userId, CancellationToken cancellationToken);

    Task<int?> GetUserIdByRefreshToken(string refreshToken, CancellationToken cancellationToken);

    Task<string?> GetRefreshTokenByUserId(int userId, CancellationToken cancellationToken);
}