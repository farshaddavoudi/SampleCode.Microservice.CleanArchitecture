using SampleMicroserviceApp.Identity.Application.Common.Contracts;
using SampleMicroserviceApp.Identity.Domain.Constants;

namespace SampleMicroserviceApp.Identity.Application.Services.SyncUsersService.Events;

public class NotifyOtherAppsUserEditedEventHandler(IMessageBrokerProducerService brokerService) : INotificationHandler<RahkaranUserEditedEvent>
{
    public async Task Handle(RahkaranUserEditedEvent notification, CancellationToken cancellationToken)
    {
        int userId = notification.SystemOldUser.Id;

        bool isUserDismissed = notification.RahkaranEditedUser.BoxId != notification.SystemOldUser.BoxId;

        if (isUserDismissed)
        {
            brokerService.PublishUserEdited(userId, RabbitMqConst.MessageRouteKey.RahkaranUserEdited("dismissed"));
        }

        bool isUserPositionChanged = notification.RahkaranEditedUser.BoxId != notification.SystemOldUser.BoxId;

        if (isUserPositionChanged)
        {
            brokerService.PublishUserEdited(userId, RabbitMqConst.MessageRouteKey.RahkaranUserEdited("position"));
        }

        // If user info changed
        if (isUserPositionChanged is false && isUserDismissed is false)
        {
            brokerService.PublishUserEdited(userId, RabbitMqConst.MessageRouteKey.RahkaranUserEdited("info"));
        }

        await Task.CompletedTask;
    }
}