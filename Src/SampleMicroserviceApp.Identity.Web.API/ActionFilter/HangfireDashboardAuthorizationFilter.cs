using Hangfire.Dashboard;
using SampleMicroserviceApp.Identity.Domain.Constants;

namespace SampleMicroserviceApp.Identity.Web.API.ActionFilter;

public class HangfireDashboardAuthorizationFilter : IDashboardAuthorizationFilter
{
    public bool Authorize(DashboardContext context)
    {
        return context.GetHttpContext().User.IsInRole(RoleConst.Identity_Administrator);
    }
}