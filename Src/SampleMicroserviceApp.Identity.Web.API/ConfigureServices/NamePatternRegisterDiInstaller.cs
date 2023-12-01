using SampleMicroserviceApp.Identity.Application;
using SampleMicroserviceApp.Identity.Application.Common.Contracts;
using SampleMicroserviceApp.Identity.Domain.ConfigurationSettings;
using SampleMicroserviceApp.Identity.Infrastructure;
using NetCore.AutoRegisterDi;

namespace SampleMicroserviceApp.Identity.Web.API.ConfigureServices;

public class NamePatternRegisterDiInstaller : IDiInstaller
{
    public void InstallServices(IServiceCollection services, AppSettings appSettings)
    {
        var assemblies = new[] {
            typeof(APIAssemblyEntryPoint).Assembly,
            typeof(InfrastructureAssemblyEntryPoint).Assembly,
            typeof(ApplicationAssemblyEntryPoint).Assembly
        };

        services.RegisterAssemblyPublicNonGenericClasses(assemblies)
            .Where(c => c.Name.EndsWith("Service"))
            .IgnoreThisInterface<ICurrentUserService>()     //optional
            .AsPublicImplementedInterfaces(ServiceLifetime.Scoped);
    }
}