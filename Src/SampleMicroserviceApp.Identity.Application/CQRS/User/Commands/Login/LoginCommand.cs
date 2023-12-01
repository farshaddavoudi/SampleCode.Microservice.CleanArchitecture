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
public class LoginCommandHandler : IRequestHandler<LoginCommand, AuthTokenDto>
{
    private readonly IRepository<UserEntity> _userRepository;
    private readonly CryptoUtility _cryptoUtility;
    private readonly AppSettings _appSettings;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMediator _mediator;
    private readonly AuthTokenUtility _authTokenUtility;

    #region ctor

    public LoginCommandHandler(CryptoUtility cryptoUtility, IRepository<UserEntity> userRepository, AppSettings appSettings, IUnitOfWork unitOfWork, IMediator mediator, AuthTokenUtility authTokenUtility)
    {
        _cryptoUtility = cryptoUtility;
        _userRepository = userRepository;
        _appSettings = appSettings;
        _unitOfWork = unitOfWork;
        _mediator = mediator;
        _authTokenUtility = authTokenUtility;
    }

    #endregion

    public async Task<AuthTokenDto> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.SingleOrDefaultAsync(
                new UserByUsernameSpec(request.Username!), cancellationToken);

        if (user is null)
            throw new BadRequestException("Invalid username or password");

        var hashedPassword = _cryptoUtility.ToHashSHA256(request.Password + user.PasswordSalt);

        if (hashedPassword != user.HashedPassword)
            throw new BadRequestException("Invalid username or password");

        var claims = _authTokenUtility.GetJwtTokenClaims(user.Id, user.FullName ?? "");

        var jwtToken = _authTokenUtility.GenerateJwtToken(claims);

        var refreshToken = _authTokenUtility.GenerateRefreshToken();

        // Set the new RefreshToken in Cache 
        // Insert the RefreshToken in History table for RefreshTokens
        // Update user LastLoggedIn prop
        await _mediator.Publish(new JwtAndRefreshTokensIssuedEvent(user.Id, jwtToken, refreshToken), cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new AuthTokenDto
        {
            RefreshToken = refreshToken,
            AccessToken = jwtToken,
            JwtExpiresAt = DateTime.Now + _appSettings.AuthSettings!.JwtTokenTtl
        };
    }
}