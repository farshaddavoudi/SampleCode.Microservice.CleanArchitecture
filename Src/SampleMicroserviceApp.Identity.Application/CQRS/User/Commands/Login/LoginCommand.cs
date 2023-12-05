using SampleMicroserviceApp.Identity.Application.Common.Contracts;
using SampleMicroserviceApp.Identity.Application.Common.Exceptions;
using SampleMicroserviceApp.Identity.Application.CQRS.User.Commands.Login.Events;
using SampleMicroserviceApp.Identity.Application.CQRS.User.DTOs;
using SampleMicroserviceApp.Identity.Application.CQRS.User.Specifications;
using SampleMicroserviceApp.Identity.Application.Services;
using SampleMicroserviceApp.Identity.Domain.ConfigurationSettings;
using SampleMicroserviceApp.Identity.Domain.Entities.User;
using SampleMicroserviceApp.Identity.Domain.Utilities;

namespace SampleMicroserviceApp.Identity.Application.CQRS.User.Commands.Login;

public record LoginCommand(string? Username, string? Password) : IRequest<AuthTokenDto>;

// Handler
public class LoginCommandHandler(
    CryptoUtility cryptoUtility,
    IRepository<UserEntity> userRepository,
    AppSettings appSettings,
    IUnitOfWork unitOfWork,
    IMediator mediator,
    AuthTokenUtility authTokenUtility
    ) : IRequestHandler<LoginCommand, AuthTokenDto>
{
    public async Task<AuthTokenDto> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.SingleOrDefaultAsync(
                new UserByUsernameSpec(request.Username!), cancellationToken);

        if (user is null)
            throw new BadRequestException("Invalid username or password");

        var hashedPassword = cryptoUtility.ToHashSHA256(request.Password + user.PasswordSalt);

        if (hashedPassword != user.HashedPassword)
            throw new BadRequestException("Invalid username or password");

        var claims = authTokenUtility.GetJwtTokenClaims(user.Id, user.FullName ?? "");

        var jwtToken = authTokenUtility.GenerateJwtToken(claims);

        var refreshToken = authTokenUtility.GenerateRefreshToken();

        // Set the new RefreshToken in Cache 
        // Insert the RefreshToken in History table for RefreshTokens
        // Update user LastLoggedIn prop
        await mediator.Publish(new JwtAndRefreshTokensIssuedEvent(user.Id, jwtToken, refreshToken), cancellationToken);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return new AuthTokenDto
        {
            RefreshToken = refreshToken,
            AccessToken = jwtToken,
            JwtExpiresAt = DateTime.Now + appSettings.AuthSettings!.JwtTokenTtl
        };
    }
}