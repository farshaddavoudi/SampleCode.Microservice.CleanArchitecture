using SampleMicroserviceApp.Identity.Domain.ConfigurationSettings;
using SampleMicroserviceApp.Identity.Domain.Shared;
using Serilog;
using Serilog.Events;
using Serilog.Exceptions;
using Serilog.Exceptions.Core;
using Serilog.Exceptions.Destructurers;
using Serilog.Exceptions.EntityFrameworkCore.Destructurers;
using Serilog.Exceptions.Refit.Destructurers;

namespace Namespace;

public static class HostBuilderExtensions
{
    public static void ConfigureSerilog(this IHostBuilder hostBuilder, AppSettings appSettings)
    {
        hostBuilder.UseSerilog((builder, loggerConfiguration) =>
        {
            loggerConfiguration
                .MinimumLevel.Information()
                // Properties
                .Enrich.FromLogContext()
                .Enrich.WithProperty("ApplicationName", AppMetadataConst.SolutionName)
                // Sinks
                .WriteTo.Console()
                .WriteTo.Debug(LogEventLevel.Error)
                .WriteTo.Seq(appSettings.SeqSettings!.SeqServerUrl!, apiKey: appSettings.SeqSettings.SeqApiKey)
                // Enrichers
                .Enrich.WithThreadId()
                .Enrich.WithMachineName()
                .Enrich.WithEnvironmentUserName()
                .Enrich.WithEnvironmentName()
                .Enrich.WithClientIp()
                .Enrich.WithExceptionDetails(new DestructuringOptionsBuilder().WithDefaultDestructurers()
                    .WithDestructurers(new List<IExceptionDestructurer>
                    {
                new DbUpdateExceptionDestructurer(),
                new ApiExceptionDestructurer(destructureCommonExceptionProperties: false)
                    }));
        });

    }
}
