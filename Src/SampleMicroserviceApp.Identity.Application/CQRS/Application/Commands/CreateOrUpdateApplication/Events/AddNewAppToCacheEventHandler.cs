using SampleMicroserviceApp.Identity.Application.Common.Contracts;

namespace SampleMicroserviceApp.Identity.Application.CQRS.Application.Commands.CreateOrUpdateApplication.Events;

public class AddNewAppToCacheEventHandler(IApplicationCacheService applicationCacheService) : INotificationHandler<ApplicationCreatedEvent>
{
    public async Task Handle(ApplicationCreatedEvent notification, CancellationToken cancellationToken)
    {
        await applicationCacheService.SetAppAsync(notification.App, cancellationToken);
    }
}