using MediatR;
using SampleMicroserviceApp.Identity.Domain.Entities.User;

namespace SampleMicroserviceApp.Identity.Application.Services.SyncUsersService.Events;

public record RahkaranUserEditedEvent(UserComparableRecord RahkaranEditedUser, UserEntity SystemOldUser)
    : INotification;