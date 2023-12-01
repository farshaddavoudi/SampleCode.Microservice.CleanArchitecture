using SampleMicroserviceApp.Identity.Application.Common.Contracts;
using SampleMicroserviceApp.Identity.Domain.ConfigurationSettings;

namespace SampleMicroserviceApp.Identity.Web.API.ConfigureServices;

public class JsonAppSettingsDiInstaller : IDiInstaller
{
    public void InstallServices(IServiceCollection services, AppSettings appSettings)
    {
        // Register (Server)AppSettings as Singleton to easy use
        services.AddSingleton(sp => appSettings);
    }
}