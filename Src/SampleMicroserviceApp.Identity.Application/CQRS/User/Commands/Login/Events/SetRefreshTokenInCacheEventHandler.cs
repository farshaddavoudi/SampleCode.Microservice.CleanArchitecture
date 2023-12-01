using MediatR;
using SampleMicroserviceApp.Identity.Application.Common.Contracts;

namespace SampleMicroserviceApp.Identity.Application.CQRS.User.Commands.Login.Events;

public class SetRefreshTokenInCacheEventHandler : INotificationHandler<JwtAndRefreshTokensIssuedEvent>
{
    private readonly IUserCacheService _userCacheService;

    #region ctor

    public SetRefreshTokenInCacheEventHandler(IUserCacheService userCacheService)
    {
        _userCacheService = userCacheService;
    }

    #endregion

    public async Task Handle(JwtAndRefreshTokensIssuedEvent notification, CancellationToken cancellationToken)
    {
        await _userCacheService.SetRefreshToken(notification.UserId, notification.RefreshToken, cancellationToken);
    }
}