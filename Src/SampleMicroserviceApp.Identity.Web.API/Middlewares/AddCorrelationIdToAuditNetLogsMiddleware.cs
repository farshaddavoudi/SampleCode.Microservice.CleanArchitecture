using SampleMicroserviceApp.Identity.Application.Common.Contracts;
using Audit.Core;

namespace SampleMicroserviceApp.Identity.Web.API.Middlewares;

public class AddCorrelationIdToAuditNetLogsMiddleware : IMiddleware
{
    private readonly ICorrelationIdManager _correlationIdManager;

    public AddCorrelationIdToAuditNetLogsMiddleware(ICorrelationIdManager correlationIdManager)
    {
        _correlationIdManager = correlationIdManager;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        // Include the trace identifier in the audit events
        Audit.Core.Configuration.AddCustomAction(ActionType.OnScopeCreated, scope =>
        {
            scope.SetCustomField("CorrelationId", _correlationIdManager.Get());
        });

        await next(context);
    }
}