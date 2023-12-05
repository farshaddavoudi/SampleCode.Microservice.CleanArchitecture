using Audit.Core;
using SampleMicroserviceApp.Identity.Application.Common.Contracts;

namespace SampleMicroserviceApp.Identity.Web.API.Middlewares;

public class AddCorrelationIdToAuditNetLogsMiddleware(ICorrelationIdManager correlationIdManager) : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        // Include the trace identifier in the audit events
        Audit.Core.Configuration.AddCustomAction(ActionType.OnScopeCreated, scope =>
        {
            scope.SetCustomField("CorrelationId", correlationIdManager.Get());
        });

        await next(context);
    }
}