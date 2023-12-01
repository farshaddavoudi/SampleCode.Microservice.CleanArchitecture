using SampleMicroserviceApp.Identity.Application.Common.Contracts;
using Serilog.Context;

namespace SampleMicroserviceApp.Identity.Web.API.Middlewares;

public class AddCorrelationIdToSerilogContextMiddleware : IMiddleware
{
    private readonly ICorrelationIdManager _correlationIdManager;

    public AddCorrelationIdToSerilogContextMiddleware(ICorrelationIdManager correlationIdManager)
    {
        _correlationIdManager = correlationIdManager;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        using (LogContext.PushProperty("CorrelationId", _correlationIdManager.Get()))
        {
            await next(context);
        }
    }
}