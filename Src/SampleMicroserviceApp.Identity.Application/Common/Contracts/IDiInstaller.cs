using SampleMicroserviceApp.Identity.Domain.ConfigurationSettings;

namespace SampleMicroserviceApp.Identity.Application.Common.Contracts;

public interface IDiInstaller
{
    void InstallServices(IServiceCollection services, AppSettings appSettings);
}