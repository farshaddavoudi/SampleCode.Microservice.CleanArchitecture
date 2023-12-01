using SampleMicroserviceApp.Identity.Application.Common.Contracts;
using FluentValidation;
using System.Reflection;
using SampleMicroserviceApp.Identity.Domain.ConfigurationSettings;

namespace SampleMicroserviceApp.Identity.Application.ConfigureServices;

public class FluentValidationDiInstaller : IDiInstaller
{
    public void InstallServices(IServiceCollection services, AppSettings appSettings)
    {
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
    }
}