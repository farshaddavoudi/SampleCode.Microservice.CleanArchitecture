using Audit.Core;
using Audit.WebApi;
using Hangfire;
using Hangfire.Dashboard;
using SampleMicroserviceApp.Identity.Domain.Shared;
using SampleMicroserviceApp.Identity.Infrastructure.BackgroundJobs;
using SampleMicroserviceApp.Identity.Infrastructure.Persistence.EFCore;

namespace SampleMicroserviceApp.Identity.Web.API.Extensions;

public static class WebApplicationExtension
{
    public static void ConfigureAuditNetLogging(this IApplicationBuilder app, AppSettings appSettings)
    {
        if (appSettings.MongoDbSettings!.IsEnabled)
        {
            // Configure the Entity framework audit.
            Audit.EntityFramework.Configuration.Setup()
            .ForContext<AppDbContext>(_ => _
                .AuditEventType("EFSaveChange:{database}")
                .IncludeEntityObjects())
            .UseOptOut();

            // Add the audit Middleware to the pipeline
            app.UseAuditMiddleware(_ => _
                .FilterByRequest(r => !r.Path.Value!.EndsWith("favicon.ico"))
                .WithEventType("API:{verb}:{controller}:{action}")
                .IncludeHeaders()
                .IncludeRequestBody()
                .IncludeResponseBody());

            // Configuring the audit output.
            // For more info, see https://github.com/thepirat000/Audit.NET#data-providers.

            Audit.Core.Configuration.Setup()
                .UseMongoDB(config => config
                    .ConnectionString(appSettings.MongoDbSettings!.ConnectionString)
                    .Database(appSettings.MongoDbSettings!.DatabaseName) //ata_{AppName}
                    .Collection(appSettings.MongoDbSettings!.CollectionName));
        }
    }

    public static void ConfigureHangfireDashboard(this IApplicationBuilder app, AppSettings appSettings)
    {
        app.UseHangfireDashboard(HangfireConst.Path, new DashboardOptions
        {
            DashboardTitle = $"{AppMetadataConst.SolutionName} JOBS",
            Authorization = new List<IDashboardAuthorizationFilter>
            {
                new HangfireDashboardAuthorizationFilter()
            }
        });
    }
}