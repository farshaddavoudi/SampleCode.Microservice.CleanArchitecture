using SampleMicroserviceApp.Identity.Application.Common.Contracts;
using SampleMicroserviceApp.Identity.Domain.ConfigurationSettings;
using SampleMicroserviceApp.Identity.Domain.Constants;

namespace SampleMicroserviceApp.Identity.Web.API.ConfigureServices;

public class AuthorizationDiInstaller : IDiInstaller
{
    /// <summary>
    /// Create Policy in order to be used in the [Authorize] attribute on Controllers
    /// [Authorize] accepts Policy as [Authorize(Policy="MyPolicy")] and Roles but not Claims
    /// </summary>
    public void InstallServices(IServiceCollection services, AppSettings appSettings)
    {
        services.AddAuthorization(options =>
        {
            //options.AddPolicy(Policy.Test, policy => policy.RequireClaim("TestClaim"));

            options.AddPolicy(PolicyConst.AdminAccessOnly, policy => policy.RequireRole(nameof(Administrator)));
        });
    }
}