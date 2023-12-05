using Audit.Core;
using SampleMicroserviceApp.Identity.Application.Common.Contracts;

namespace SampleMicroserviceApp.Identity.Web.API.Middlewares;

public class AddCurrentUserToAuditNetLogsMiddleware(ICurrentUserService currentUserService) : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        // Include the trace identifier in the audit events
        if (context.User.Identity?.IsAuthenticated ?? false)
        {
            Audit.Core.Configuration.AddCustomAction(ActionType.OnScopeCreated, scope =>
            {
                scope.SetCustomField("UserId", currentUserService.User()?.Id);
                scope.SetCustomField("User", currentUserService.User(), true);
            });
        }

        await next(context);
    }
}