using SampleMicroserviceApp.Identity.Application.Common.Contracts;
using Serilog.Context;

namespace SampleMicroserviceApp.Identity.Web.API.Middlewares;

public class AddCurrentUserToSerilogContextMiddleware : IMiddleware
{
    private readonly ICurrentUserService _currentUserService;

    public AddCurrentUserToSerilogContextMiddleware(ICurrentUserService currentUserService)
    {
        _currentUserService = currentUserService;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        using (LogContext.PushProperty("User", _currentUserService.User(), true))
        {
            await next(context);
        }
    }
}