using AutoMapper;
using MediatR;
using SampleMicroserviceApp.Identity.Application.Common.Contracts;
using SampleMicroserviceApp.Identity.Application.CQRS.User.DTOs;

namespace SampleMicroserviceApp.Identity.Application.Services.SyncUsersService.Events;

public class ResetEditedUserCachesEventHandler : INotificationHandler<RahkaranUserEditedEvent>
{
    private readonly IUserCacheService _userCacheService;
    private readonly IMapper _mapper;

    #region ctor

    public ResetEditedUserCachesEventHandler(IUserCacheService userCacheService, IMapper mapper)
    {
        _userCacheService = userCacheService;
        _mapper = mapper;
    }

    #endregion

    public async Task Handle(RahkaranUserEditedEvent notification, CancellationToken cancellationToken)
    {
        await _userCacheService.SetUserAsync(_mapper.Map<UserMiniDto>(notification.RahkaranEditedUser), cancellationToken);
    }
}