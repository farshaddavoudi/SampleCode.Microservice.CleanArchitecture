using SampleMicroserviceApp.Identity.Application.Common.Contracts;

namespace SampleMicroserviceApp.Identity.Application.CQRS.User.Commands.Login.Events;

public class SetRefreshTokenInCacheEventHandler(IUserCacheService userCacheService) : INotificationHandler<JwtAndRefreshTokensIssuedEvent>
{
    public async Task Handle(JwtAndRefreshTokensIssuedEvent notification, CancellationToken cancellationToken)
    {
        await userCacheService.SetRefreshToken(notification.UserId, notification.RefreshToken, cancellationToken);
    }
}