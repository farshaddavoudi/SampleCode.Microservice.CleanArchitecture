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

public class GenerateNewTokenCommandHandler(
    IUserCacheService userCacheService,
    AuthTokenUtility authTokenUtility,
    IMediator mediator,
    IUnitOfWork unitOfWork,
    AppSettings appSettings,
    IRepository<UserEntity> userRepository
    ) : IRequestHandler<GenerateNewTokenCommand, AuthTokenDto>
{
    public async Task<AuthTokenDto> Handle(GenerateNewTokenCommand request, CancellationToken cancellationToken)
    {
        // Get UserId from RefreshToken

        var userId = await userCacheService.GetUserIdByRefreshToken(request.RefreshToken!, cancellationToken);

        if (userId.HasValue is false)
            throw new BadRequestException("Invalid RefreshToken");

        // The RefreshToken is valid, let's create new tokens

        var userFullName = await userRepository.FirstOrDefaultAsync(new UserFullNameByIdSpec(userId.Value), cancellationToken);

        var claims = authTokenUtility.GetJwtTokenClaims(userId.Value, userFullName ?? "");

        var jwtToken = authTokenUtility.GenerateJwtToken(claims);

        var refreshToken = authTokenUtility.GenerateRefreshToken();

        await mediator.Publish(new JwtAndRefreshTokensIssuedEvent(userId.Value, jwtToken, refreshToken), cancellationToken);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return new AuthTokenDto
        {
            RefreshToken = refreshToken,
            AccessToken = jwtToken,
            JwtExpiresAt = DateTime.Now + appSettings.AuthSettings!.JwtTokenTtl
        };
    }
}