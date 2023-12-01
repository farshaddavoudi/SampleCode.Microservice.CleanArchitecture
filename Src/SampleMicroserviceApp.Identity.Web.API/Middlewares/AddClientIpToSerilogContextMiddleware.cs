using SampleMicroserviceApp.Identity.Application.Common.Contracts;
using SampleMicroserviceApp.Identity.Web.API.Extensions;
using Serilog.Context;

namespace SampleMicroserviceApp.Identity.Web.API.Middlewares;

public class AddClientIpToSerilogContextMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var clientIp = context.GetClientIp();

        using (LogContext.PushProperty("IP", clientIp ?? "unknown", true))
        {
            await next(context);
        }
    }
}