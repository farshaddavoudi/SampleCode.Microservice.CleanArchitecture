using SampleMicroserviceApp.Identity.Application.Common.Contracts;
using SampleMicroserviceApp.Identity.Application.Common.Exceptions;
using SampleMicroserviceApp.Identity.Domain.Entities.User;

namespace SampleMicroserviceApp.Identity.Application.CQRS.User.Commands.Login.Events;

public class UpdateUserLoggedInPropertyEventHandler(IRepository<UserEntity> userRepository) : INotificationHandler<JwtAndRefreshTokensIssuedEvent>
{
    public async Task Handle(JwtAndRefreshTokensIssuedEvent notification, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByIdAsync(notification.UserId, cancellationToken);

        if (user is null)
            throw new NotFoundException("User not found");

        user.LastLoggedIn = DateTime.Now;

        await userRepository.UpdateAsync(user, cancellationToken, false);
    }
}