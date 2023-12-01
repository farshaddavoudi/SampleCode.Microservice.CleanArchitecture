namespace SampleMicroserviceApp.Identity.Web.API.Extensions;

public static class MiddlewareExtension
{
    private const string ClientIpCustomHeaderName = "X-Client-IP";

    public static string? GetClientIp(this HttpContext context)
    {
        // Get external network call's IP address [Behind WAF]
        string? ip = context.Request.Headers[ClientIpCustomHeaderName].FirstOrDefault();

        if (ip is null) //Internal network
        {
            // Get internal network call's IP address
            ip = context.Connection.RemoteIpAddress?.ToString();
        }

        return ip;
    }
}