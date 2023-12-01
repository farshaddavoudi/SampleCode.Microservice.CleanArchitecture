using SampleMicroserviceApp.Identity.Application.Common.Contracts;
using SampleMicroserviceApp.Identity.Application.Common.Implementations;
using SampleMicroserviceApp.Identity.Domain.ConfigurationSettings;

namespace SampleMicroserviceApp.Identity.Application.ConfigureServices;

public class CorrelationIdManagerDiInstaller : IDiInstaller
{
    public void InstallServices(IServiceCollection services, AppSettings appSettings)
    {
        services.AddScoped<ICorrelationIdManager, CorrelationIdManager>();
    }
}