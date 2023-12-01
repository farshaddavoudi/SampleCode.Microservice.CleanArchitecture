using MediatR;
using SampleMicroserviceApp.Identity.Application.Common.Contracts;
using SampleMicroserviceApp.Identity.Application.Common.Exceptions;
using SampleMicroserviceApp.Identity.Domain.Entities.User;
using SampleMicroserviceApp.Identity.Domain.Utilities;

namespace SampleMicroserviceApp.Identity.Application.CQRS.User.Commands.RegisterRahkaranUser;

public record RegisterRahkaranUserCommand(int UserId, string Username, string Password) : IRequest;

// Handler
public class RegisterRahkaranUserCommandHandler : IRequestHandler<RegisterRahkaranUserCommand>
{
    private readonly IRepository<UserEntity> _userRepository;
    private readonly CryptoUtility _cryptoUtility;

    #region ctor

    public RegisterRahkaranUserCommandHandler(IRepository<UserEntity> userRepository, CryptoUtility cryptoUtility)
    {
        _userRepository = userRepository;
        _cryptoUtility = cryptoUtility;
    }

    #endregion

    public async Task Handle(RegisterRahkaranUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.UserId, cancellationToken);

        if (user is null)
            throw new NotFoundException("No user was found");

        if (user.IsRegistered)
            throw new BadRequestException("This user is already registered");

        user.UserName = request.Username;

        var passSalt = Guid.NewGuid().ToString();

        user.PasswordSalt = passSalt;

        user.HashedPassword = _cryptoUtility.ToHashSHA256(request.Password + passSalt);

        user.PasswordSetAt = DateTime.Now;

        user.IsRegistered = true;

        await _userRepository.UpdateAsync(user, cancellationToken);

        // TODO: Send SMS to user with username / password
        // TODO: RabbitMQ event for creating a new user
    }
}
