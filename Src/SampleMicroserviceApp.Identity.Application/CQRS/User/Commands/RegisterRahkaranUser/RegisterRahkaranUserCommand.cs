using SampleMicroserviceApp.Identity.Application.Common.Contracts;
using SampleMicroserviceApp.Identity.Application.Common.Exceptions;
using SampleMicroserviceApp.Identity.Domain.Entities.User;
using SampleMicroserviceApp.Identity.Domain.Utilities;

namespace SampleMicroserviceApp.Identity.Application.CQRS.User.Commands.RegisterRahkaranUser;

public record RegisterRahkaranUserCommand(int UserId, string Username, string Password) : IRequest;

// Handler
public class RegisterRahkaranUserCommandHandler(IRepository<UserEntity> userRepository, CryptoUtility cryptoUtility) : IRequestHandler<RegisterRahkaranUserCommand>
{
    public async Task Handle(RegisterRahkaranUserCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByIdAsync(request.UserId, cancellationToken);

        if (user is null)
            throw new NotFoundException("No user was found");

        if (user.IsRegistered)
            throw new BadRequestException("This user is already registered");

        user.UserName = request.Username;

        var passSalt = Guid.NewGuid().ToString();

        user.PasswordSalt = passSalt;

        user.HashedPassword = cryptoUtility.ToHashSHA256(request.Password + passSalt);

        user.PasswordSetAt = DateTime.Now;

        user.IsRegistered = true;

        await userRepository.UpdateAsync(user, cancellationToken);

        // TODO: Send SMS to user with username / password
        // TODO: RabbitMQ event for creating a new user
    }
}
