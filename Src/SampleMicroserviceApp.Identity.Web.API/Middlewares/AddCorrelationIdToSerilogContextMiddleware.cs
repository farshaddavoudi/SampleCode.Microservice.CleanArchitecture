using SampleMicroserviceApp.Identity.Application.Common.Contracts;
using Serilog.Context;

namespace SampleMicroserviceApp.Identity.Web.API.Middlewares;

public class AddCorrelationIdToSerilogContextMiddleware(ICorrelationIdManager correlationIdManager) : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        using (LogContext.PushProperty("CorrelationId", correlationIdManager.Get()))
        {
            await next(context);
        }
    }
}