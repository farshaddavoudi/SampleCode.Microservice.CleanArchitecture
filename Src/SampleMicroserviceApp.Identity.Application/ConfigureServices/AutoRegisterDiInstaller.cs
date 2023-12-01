using SampleMicroserviceApp.Identity.Application.Common.Contracts;
using SampleMicroserviceApp.Identity.Domain.ConfigurationSettings;
using NetCore.AutoRegisterDi;

namespace SampleMicroserviceApp.Identity.Application.ConfigureServices;

/// <summary>
/// Register Assembly Public NonAbstract Classes 
/// </summary>
public class AutoRegisterDiInstaller : IDiInstaller
{
    public void InstallServices(IServiceCollection services, AppSettings appSettings)
    {
        services.RegisterAssemblyPublicNonGenericClasses()
            .Where(c => c.Name.EndsWith("Service"))  //optional
        //    .IgnoreThisInterface<IMyInterface>()     //optional
              .AsPublicImplementedInterfaces();

        #region Scanning Multiple assemblies example

        //var assembliesToScan = new[]
        //{
        //    Assembly.GetExecutingAssembly(),
        //    Assembly.GetAssembly(typeof(MyServiceInAssembly1)),
        //    Assembly.GetAssembly(typeof(MyServiceInAssembly2))
        //};

        //services.RegisterAssemblyPublicNonGenericClasses(assembliesToScan)
        //    .Where(c => c.Name.EndsWith("Service"))  //optional
        //    .IgnoreThisInterface<IMyInterface>()     //optional
        //    .AsPublicImplementedInterfaces();

        #endregion
    }
}