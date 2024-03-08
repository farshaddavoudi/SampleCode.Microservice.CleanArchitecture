using Hangfire;
using Hangfire.PostgreSql;
using SampleMicroserviceApp.Identity.Application.Common.Contracts;
using SampleMicroserviceApp.Identity.Domain.ConfigurationSettings;
using SampleMicroserviceApp.Identity.Infrastructure.BackgroundJobs;

namespace SampleMicroserviceApp.Identity.Infrastructure.ConfigureServices;

public class HangfireDiInstaller : IDiInstaller
{
    public void InstallServices(IServiceCollection services, AppSettings appSettings)
    {
        // Add Hangfire services.
        services.AddHangfire(config =>
        {
            config
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UsePostgreSqlStorage(c => c.UseNpgsqlConnection(appSettings.ConnStrSettings!.HangfireConnStr));
        });

        // Add the processing server as IHostedService
        services.AddHangfireServer(options =>
        {
            var env = appSettings.IsDevelopment ? "DEV" : "PROD";
            options.ServerName = $"Core.Identity–{env}–SERVER";
            options.Queues = new[] { HangfireConst.Queue.DefaultQueue };
        });
    }
}