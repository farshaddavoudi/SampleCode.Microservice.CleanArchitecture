using MediatR;
using SampleMicroserviceApp.Identity.Application.Common.Contracts;

namespace SampleMicroserviceApp.Identity.Application.CQRS.Application.Commands.CreateOrUpdateApplication.Events;

public class AddNewAppToCacheEventHandler : INotificationHandler<ApplicationCreatedEvent>
{
    private readonly IApplicationCacheService _applicationCacheService;

    #region ctor

    public AddNewAppToCacheEventHandler(IApplicationCacheService applicationCacheService)
    {
        _applicationCacheService = applicationCacheService;
    }

    #endregion

    public async Task Handle(ApplicationCreatedEvent notification, CancellationToken cancellationToken)
    {
        await _applicationCacheService.SetAppAsync(notification.App, cancellationToken);
    }
}