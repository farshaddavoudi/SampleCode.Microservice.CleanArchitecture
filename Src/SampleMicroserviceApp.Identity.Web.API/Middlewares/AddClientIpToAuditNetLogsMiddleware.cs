using SampleMicroserviceApp.Identity.Application.Common.Contracts;
using SampleMicroserviceApp.Identity.Web.API.Extensions;
using Audit.Core;

namespace SampleMicroserviceApp.Identity.Web.API.Middlewares;

public class AddClientIpToAuditNetLogsMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var clientIp = context.GetClientIp();

        Audit.Core.Configuration.AddCustomAction(ActionType.OnScopeCreated, scope =>
        {
            scope.SetCustomField("IP", clientIp ?? "unknown");
        });

        await next(context);
    }
}