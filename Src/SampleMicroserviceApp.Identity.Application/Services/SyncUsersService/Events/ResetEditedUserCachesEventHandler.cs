using AutoMapper;
using SampleMicroserviceApp.Identity.Application.Common.Contracts;
using SampleMicroserviceApp.Identity.Application.CQRS.User.DTOs;

namespace SampleMicroserviceApp.Identity.Application.Services.SyncUsersService.Events;

public class ResetEditedUserCachesEventHandler(IUserCacheService userCacheService, IMapper mapper) : INotificationHandler<RahkaranUserEditedEvent>
{
    public async Task Handle(RahkaranUserEditedEvent notification, CancellationToken cancellationToken)
    {
        await userCacheService.SetUserAsync(mapper.Map<UserMiniDto>(notification.RahkaranEditedUser), cancellationToken);
    }
}