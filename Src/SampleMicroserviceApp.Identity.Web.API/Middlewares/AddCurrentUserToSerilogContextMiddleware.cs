using SampleMicroserviceApp.Identity.Application.Common.Contracts;
using Serilog.Context;

namespace SampleMicroserviceApp.Identity.Web.API.Middlewares;

public class AddCurrentUserToSerilogContextMiddleware(ICurrentUserService currentUserService) : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        using (LogContext.PushProperty("User", currentUserService.User(), true))
        {
            await next(context);
        }
    }
}