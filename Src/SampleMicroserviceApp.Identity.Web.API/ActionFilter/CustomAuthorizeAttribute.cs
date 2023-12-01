using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using SampleMicroserviceApp.Identity.Application.Common.Contracts;
using SampleMicroserviceApp.Identity.Domain.Shared;

namespace SampleMicroserviceApp.Identity.Web.API.ActionFilter;

public class CustomAuthorizeAttribute : AuthorizeAttribute, IAsyncAuthorizationFilter
{
    public string[] Claims { get; set; } = Array.Empty<string>();
    public new string[] Roles { get; set; } = Array.Empty<string>();

    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        bool skipAuthorization = context.Filters.OfType<IAllowAnonymousFilter>().Any() ||
                                 context.ActionDescriptor.EndpointMetadata.Any(m => m.GetType().Name == nameof(AllowAnonymousAttribute)) ||
                                 context.ActionDescriptor.EndpointMetadata.Any(m => m.GetType().Name == nameof(AllowAnonymousAttribute));

        if (skipAuthorization) return;

        var currentUserService = context.HttpContext.RequestServices.GetRequiredService<ICurrentUserService>();

        if (currentUserService.IsAuthenticated() is false)
        {
            context.Result = new ForbidResult();
            return;
        }

        if (currentUserService.IsAdmin())
            return;

        var userPermissionsService = context.HttpContext.RequestServices.GetRequiredService<IUserPermissionsService>();

        var hasAccess = await userPermissionsService.HasUserAccessToResource(
            currentUserService.User()!.Id,
            AppMetadataConst.AppKey,
            Claims.ToList(), Roles.ToList(),
            CancellationToken.None);

        if (hasAccess is false)
        {
            context.Result = new ForbidResult();
        }
    }
}