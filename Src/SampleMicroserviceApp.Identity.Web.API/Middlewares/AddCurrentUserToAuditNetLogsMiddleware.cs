using Audit.Core;
using SampleMicroserviceApp.Identity.Application.Common.Contracts;

namespace SampleMicroserviceApp.Identity.Web.API.Middlewares;

public class AddCurrentUserToAuditNetLogsMiddleware : IMiddleware
{
    private readonly ICurrentUserService _currentUserService;

    public AddCurrentUserToAuditNetLogsMiddleware(ICurrentUserService currentUserService)
    {
        _currentUserService = currentUserService;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        // Include the trace identifier in the audit events
        if (context.User.Identity?.IsAuthenticated ?? false)
        {
            Audit.Core.Configuration.AddCustomAction(ActionType.OnScopeCreated, scope =>
            {
                scope.SetCustomField("UserId", _currentUserService.User()?.Id);
                scope.SetCustomField("User", _currentUserService.User(), true);
            });
        }

        await next(context);
    }
}