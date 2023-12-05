using SampleMicroserviceApp.Identity.Application.Common.Contracts;
using SampleMicroserviceApp.Identity.Domain.ConfigurationSettings;
using SampleMicroserviceApp.Identity.Domain.Entities.User;

namespace SampleMicroserviceApp.Identity.Application.CQRS.User.Commands.Login.Events;

public class AddRefreshTokenHistoryEventHandler(IRepository<RefreshTokenHistoryEntity> refreshTokenHistoryRepository, AppSettings appSettings)
    : INotificationHandler<JwtAndRefreshTokensIssuedEvent>
{
    public async Task Handle(JwtAndRefreshTokensIssuedEvent notification, CancellationToken cancellationToken)
    {
        await refreshTokenHistoryRepository.AddAsync(new RefreshTokenHistoryEntity
        {
            RefreshToken = notification.RefreshToken,
            ExpiresAt = DateTime.Now + appSettings.AuthSettings!.RefreshTokenTtl,
            IsValid = true,
            UserId = notification.UserId
        }, cancellationToken, false);
    }
}