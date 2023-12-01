using SampleMicroserviceApp.Identity.Application.Common.Contracts;
using SampleMicroserviceApp.Identity.Domain.ConfigurationSettings;
using SampleMicroserviceApp.Identity.Infrastructure.UserIdentity;

namespace SampleMicroserviceApp.Identity.Web.API.ConfigureServices;

public class CurrentUserDiInstaller : IDiInstaller
{
    public void InstallServices(IServiceCollection services, AppSettings appSettings)
    {
        services.AddScoped<ICurrentUserService, CurrentUserService>();
    }
}