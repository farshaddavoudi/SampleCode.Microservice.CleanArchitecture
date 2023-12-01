// ReSharper disable CheckNamespace

using SampleMicroserviceApp.Identity.Application;
using SampleMicroserviceApp.Identity.Application.Common.Contracts;
using SampleMicroserviceApp.Identity.Domain.ConfigurationSettings;
using SampleMicroserviceApp.Identity.Infrastructure;

namespace SampleMicroserviceApp.Identity.Web.API.Extensions;

public static class ConfigureServicesExtension
{
    public static IServiceCollection ConfigureServicesWithInstallers(this IServiceCollection services, AppSettings appSettings)
    {
        var assemblies = new[] {
            typeof(APIAssemblyEntryPoint).Assembly,
            typeof(InfrastructureAssemblyEntryPoint).Assembly,
            typeof(ApplicationAssemblyEntryPoint).Assembly
        };

        var installers = assemblies.SelectMany(a => a.GetExportedTypes())
            .Where(c => c.IsClass && !c.IsAbstract && c.IsPublic && typeof(IDiInstaller).IsAssignableFrom(c))
            .Select(Activator.CreateInstance).Cast<IDiInstaller>().ToList();

        installers.ForEach(i => i.InstallServices(services, appSettings));

        return services;
    }
}