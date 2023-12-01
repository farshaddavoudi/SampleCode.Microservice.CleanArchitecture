using MediatR;
using SampleMicroserviceApp.Identity.Application.Common.Contracts;
using SampleMicroserviceApp.Identity.Domain.ConfigurationSettings;
using SampleMicroserviceApp.Identity.Domain.Entities.User;

namespace SampleMicroserviceApp.Identity.Application.CQRS.User.Commands.Login.Events;

public class AddRefreshTokenHistoryEventHandler : INotificationHandler<JwtAndRefreshTokensIssuedEvent>
{
    private readonly IRepository<RefreshTokenHistoryEntity> _refreshTokenHistoryRepository;
    private readonly AppSettings _appSettings;

    #region ctor

    public AddRefreshTokenHistoryEventHandler(IRepository<RefreshTokenHistoryEntity> refreshTokenHistoryRepository, AppSettings appSettings)
    {
        _refreshTokenHistoryRepository = refreshTokenHistoryRepository;
        _appSettings = appSettings;
    }

    #endregion

    public async Task Handle(JwtAndRefreshTokensIssuedEvent notification, CancellationToken cancellationToken)
    {
        await _refreshTokenHistoryRepository.AddAsync(new RefreshTokenHistoryEntity
        {
            RefreshToken = notification.RefreshToken,
            ExpiresAt = DateTime.Now + _appSettings.AuthSettings!.RefreshTokenTtl,
            IsValid = true,
            UserId = notification.UserId
        }, cancellationToken, false);
    }
}