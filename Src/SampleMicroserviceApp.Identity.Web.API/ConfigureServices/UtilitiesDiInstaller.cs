using SampleMicroserviceApp.Identity.Application.Common.Contracts;
using SampleMicroserviceApp.Identity.Application.Services;
using SampleMicroserviceApp.Identity.Domain.ConfigurationSettings;
using SampleMicroserviceApp.Identity.Domain.Utilities;

namespace SampleMicroserviceApp.Identity.Web.API.ConfigureServices;

public class UtilitiesDiInstaller : IDiInstaller
{
    public void InstallServices(IServiceCollection services, AppSettings appSettings)
    {
        services.AddSingleton<CryptoUtility>();

        services.AddSingleton<AuthTokenUtility>();
    }
}