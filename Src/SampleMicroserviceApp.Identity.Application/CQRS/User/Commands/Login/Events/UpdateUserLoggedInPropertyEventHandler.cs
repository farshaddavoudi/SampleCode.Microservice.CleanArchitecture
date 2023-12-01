using MediatR;
using SampleMicroserviceApp.Identity.Application.Common.Contracts;
using SampleMicroserviceApp.Identity.Application.Common.Exceptions;
using SampleMicroserviceApp.Identity.Domain.Entities.User;

namespace SampleMicroserviceApp.Identity.Application.CQRS.User.Commands.Login.Events;

public class UpdateUserLoggedInPropertyEventHandler : INotificationHandler<JwtAndRefreshTokensIssuedEvent>
{
    private readonly IRepository<UserEntity> _userRepository;

    #region ctor

    public UpdateUserLoggedInPropertyEventHandler(IRepository<UserEntity> userRepository)
    {
        _userRepository = userRepository;
    }

    #endregion

    public async Task Handle(JwtAndRefreshTokensIssuedEvent notification, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(notification.UserId, cancellationToken);

        if (user is null)
            throw new NotFoundException("User not found");

        user.LastLoggedIn = DateTime.Now;

        await _userRepository.UpdateAsync(user, cancellationToken, false);
    }
}