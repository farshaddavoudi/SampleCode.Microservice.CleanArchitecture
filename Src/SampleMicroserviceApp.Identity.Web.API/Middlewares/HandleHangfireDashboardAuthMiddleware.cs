using SampleMicroserviceApp.Identity.Domain.ConfigurationSettings;
using SampleMicroserviceApp.Identity.Domain.Constants;
using SampleMicroserviceApp.Identity.Domain.Extensions;

namespace SampleMicroserviceApp.Identity.Web.API.Middlewares;

public class HandleHangfireDashboardAuthMiddleware : IMiddleware
{
    private readonly AppSettings _appSettings;

    #region ctor

    public HandleHangfireDashboardAuthMiddleware(AppSettings appSettings)
    {
        _appSettings = appSettings;
    }

    #endregion

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        // Check if the request path contains the string "hangfire"
        if (context.Request.Path.ToString().Contains(HangfireConst.Path))
        {
            // Get the access_token from the query string
            string? accessToken = GetAccessTokenFromQueryOrCookie(context);

            // Add the access_token to the Authorization header
            if (accessToken.IsNotNullOrWhitespace())
            {
                context.Request.Headers.Add("Authorization", $"Bearer {accessToken}");
            }
        }

        await next(context);
    }

    private string? GetAccessTokenFromQueryOrCookie(HttpContext httpContext)
    {
        string? jwtToken;

        if (httpContext.Request.Query.ContainsKey("access_token"))
        {
            jwtToken = httpContext.Request.Query["access_token"].FirstOrDefault();

            if (jwtToken.IsNotNullOrWhitespace())
            {
                // This makes subsequent requests following the main request (css requests) have also the access token
                SetHangfireCookieForAuth(jwtToken!, httpContext);
            }
        }
        else
        {
            jwtToken = httpContext.Request.Cookies["_hangfireCookie"];
        }

        return jwtToken;
    }

    private void SetHangfireCookieForAuth(string jwtToken, HttpContext httpContext)
    {
        httpContext.Response.Cookies.Append("_hangfireCookie", jwtToken,
            new CookieOptions { Expires = DateTime.Now.Add(_appSettings.AuthSettings!.JwtTokenTtl) });
    }
}