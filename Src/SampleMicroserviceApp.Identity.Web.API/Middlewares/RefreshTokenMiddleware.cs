using SampleMicroserviceApp.Identity.Application.Common.Contracts;
using SampleMicroserviceApp.Identity.Domain.ConfigurationSettings;

namespace SampleMicroserviceApp.Identity.Web.API.Middlewares;

public class RefreshTokenMiddleware : IMiddleware
{
    private readonly IUserCacheService _userCacheService;
    private readonly AppSettings _appSettings;

    #region ctor

    public RefreshTokenMiddleware(IUserCacheService userCacheService, AppSettings appSettings)
    {
        _userCacheService = userCacheService;
        _appSettings = appSettings;
    }

    #endregion

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        // Asking for RefreshToken in development is unnecessary. Moreover, it prevents using Swagger as we cannot send custom header in Swagger
        // AccessToken TTL is development is 1 day and won't expire soon
        if (_appSettings.IsProduction && (context.User.Identity?.IsAuthenticated ?? false))
        {
            var refreshToken = context.Request.Headers[_appSettings.AuthSettings!.RefreshTokenHeaderName].FirstOrDefault();

            if (string.IsNullOrWhiteSpace(refreshToken))
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync("Refresh token is missing.");
                return;
            }

            var userId = await _userCacheService.GetUserIdByRefreshToken(refreshToken, CancellationToken.None);

            if (userId.HasValue is false)
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync("Refresh token is invalid.");
                return;
            }
        }

        await next(context);
    }
}