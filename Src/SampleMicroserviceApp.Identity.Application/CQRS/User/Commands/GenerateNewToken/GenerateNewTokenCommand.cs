using SampleMicroserviceApp.Identity.Application.Common.Contracts;
using SampleMicroserviceApp.Identity.Application.Common.Exceptions;
using SampleMicroserviceApp.Identity.Application.CQRS.User.Commands.Login.Events;
using SampleMicroserviceApp.Identity.Application.CQRS.User.DTOs;
using SampleMicroserviceApp.Identity.Application.CQRS.User.Specifications;
using SampleMicroserviceApp.Identity.Application.Services;
using SampleMicroserviceApp.Identity.Domain.ConfigurationSettings;
using SampleMicroserviceApp.Identity.Domain.Entities.User;

namespace SampleMicroserviceApp.Identity.Application.CQRS.User.Commands.GenerateNewToken;

public record GenerateNewTokenCommand(string? AccessToken, string? RefreshToken) : IRequest<AuthTokenDto>;

public class GenerateNewTokenCommandHandler : IRequestHandler<GenerateNewTokenCommand, AuthTokenDto>
{
    private readonly IUserCacheService _userCacheService;
    private readonly IRepository<UserEntity> _userRepository;
    private readonly AuthTokenUtility _authTokenUtility;
    private readonly IMediator _mediator;
    private readonly IUnitOfWork _unitOfWork;
    private readonly AppSettings _appSettings;

    #region ctor

    public GenerateNewTokenCommandHandler(IUserCacheService userCacheService, AuthTokenUtility authTokenUtility, IMediator mediator, IUnitOfWork unitOfWork, AppSettings appSettings, IRepository<UserEntity> userRepository)
    {
        _userCacheService = userCacheService;
        _authTokenUtility = authTokenUtility;
        _mediator = mediator;
        _unitOfWork = unitOfWork;
        _appSettings = appSettings;
        _userRepository = userRepository;
    }

    #endregion

    public async Task<AuthTokenDto> Handle(GenerateNewTokenCommand request, CancellationToken cancellationToken)
    {
        // Get UserId from RefreshToken

        var userId = await _userCacheService.GetUserIdByRefreshToken(request.RefreshToken!, cancellationToken);

        if (userId.HasValue is false)
            throw new BadRequestException("Invalid RefreshToken");

        // The RefreshToken is valid, let's create new tokens

        var userFullName = await _userRepository.FirstOrDefaultProjectedAsync(new UserFullNameByIdSpec(userId.Value), cancellationToken);

        var claims = _authTokenUtility.GetJwtTokenClaims(userId.Value, userFullName ?? "");

        var jwtToken = _authTokenUtility.GenerateJwtToken(claims);

        var refreshToken = _authTokenUtility.GenerateRefreshToken();

        await _mediator.Publish(new JwtAndRefreshTokensIssuedEvent(userId.Value, jwtToken, refreshToken), cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new AuthTokenDto
        {
            RefreshToken = refreshToken,
            AccessToken = jwtToken,
            JwtExpiresAt = DateTime.Now + _appSettings.AuthSettings!.JwtTokenTtl
        };
    }
}