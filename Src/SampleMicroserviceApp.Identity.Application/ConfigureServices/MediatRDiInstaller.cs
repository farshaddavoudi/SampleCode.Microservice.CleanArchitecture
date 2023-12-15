using MediatR.NotificationPublishers;
using SampleMicroserviceApp.Identity.Application.Common.Contracts;
using SampleMicroserviceApp.Identity.Application.Common.PipelineBehaviours;
using SampleMicroserviceApp.Identity.Domain.ConfigurationSettings;

namespace SampleMicroserviceApp.Identity.Application.ConfigureServices;

public class MediatRDiInstaller : IDiInstaller
{
    public void InstallServices(IServiceCollection services, AppSettings appSettings)
    {
        services.AddMediatR(configs =>
        {
            configs.RegisterServicesFromAssemblies(typeof(ApplicationAssemblyEntryPoint).Assembly);

            configs.Lifetime = ServiceLifetime.Scoped;

            configs.NotificationPublisher = new TaskWhenAllPublisher();

            // Pipelines order matters. MediatR executes them from top to bottom 
            configs.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>), ServiceLifetime.Scoped);
            configs.AddBehavior(typeof(IPipelineBehavior<,>), typeof(DbTransactionBehaviour<,>), ServiceLifetime.Scoped);
        });
    }
}