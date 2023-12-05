using SampleMicroserviceApp.Identity.Application.Common.Contracts;

namespace SampleMicroserviceApp.Identity.Web.API.Middlewares;

public class RefreshTokenMiddleware(IUserCacheService userCacheService, AppSettings appSettings) : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        // Asking for RefreshToken in development is unnecessary. Moreover, it prevents using Swagger as we cannot send custom header in Swagger
        // AccessToken TTL is development is 1 day and won't expire soon
        if (appSettings.IsProduction && (context.User.Identity?.IsAuthenticated ?? false))
        {
            var refreshToken = context.Request.Headers[appSettings.AuthSettings!.RefreshTokenHeaderName].FirstOrDefault();

            if (string.IsNullOrWhiteSpace(refreshToken))
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync("Refresh token is missing.");
                return;
            }

            var userId = await userCacheService.GetUserIdByRefreshToken(refreshToken, CancellationToken.None);

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