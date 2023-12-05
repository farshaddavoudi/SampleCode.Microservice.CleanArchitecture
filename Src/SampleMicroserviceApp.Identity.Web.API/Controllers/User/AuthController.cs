using SampleMicroserviceApp.Identity.Application.Common.Exceptions;
using SampleMicroserviceApp.Identity.Application.CQRS.Application.Queries.GetClientAppUserClaims;
using SampleMicroserviceApp.Identity.Application.CQRS.User.Commands.GenerateNewToken;
using SampleMicroserviceApp.Identity.Application.CQRS.User.Commands.KickOutUser;
using SampleMicroserviceApp.Identity.Application.CQRS.User.Commands.Login;
using SampleMicroserviceApp.Identity.Application.CQRS.User.Commands.RegisterRahkaranUser;

namespace SampleMicroserviceApp.Identity.Web.API.Controllers.User;

public class AuthController(IMediator mediator, AppSettings appSettings) : BaseApiController
{
    [HttpPost, AllowAnonymous]
    public async Task<IActionResult> Login(LoginCommand command, CancellationToken cancellationToken)
    {
        var loginResult = await mediator.Send(command, cancellationToken);

        return Ok(loginResult);
    }

    [HttpPost, AllowAnonymous]
    public async Task<IActionResult> GenerateNewToken(CancellationToken cancellationToken)
    {
        if (HttpContext.Request.Headers.TryGetValue(appSettings.AuthSettings!.RefreshTokenHeaderName,
               out var refreshToken))
        {
            GenerateNewTokenCommand command = new(null, refreshToken);

            var tokenResult = await mediator.Send(command, cancellationToken);

            return Ok(tokenResult);
        }

        throw new BadRequestException("Refresh token is missing.");
    }

    [CustomAuthorize(Claims = new[] { ClaimConst.Identity_User_Manage })]
    [HttpPost]
    public async Task<IActionResult> RegisterRahkaranUser(RegisterRahkaranUserCommand command, CancellationToken cancellationToken)
    {
        await mediator.Send(command, cancellationToken);

        return NoContent();
    }

    [CustomAuthorize(Claims = new[] { ClaimConst.Identity_User_Manage })]
    [HttpPost]
    public async Task<IActionResult> KickOutUser(KickOutUserCommand command, CancellationToken cancellationToken)
    {
        await mediator.Send(command, cancellationToken);

        return NoContent();
    }

    [HttpGet("{clientAppKey}")]
    public async Task<IActionResult> GetClientAppUserAllClaims(string clientAppKey, CancellationToken cancellationToken)
    {
        var userClaims = await mediator.Send(new GetClientAppUserAllClaimsQuery(clientAppKey), cancellationToken);

        return Ok(userClaims);
    }
}