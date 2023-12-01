using Audit.EntityFramework;
using Audit.EntityFramework.Interceptors;
using Microsoft.EntityFrameworkCore;
using SampleMicroserviceApp.Identity.Application.Common.Contracts;
using SampleMicroserviceApp.Identity.Domain.ConfigurationSettings;
using SampleMicroserviceApp.Identity.Infrastructure.Persistence.EFCore;
using SampleMicroserviceApp.Identity.Infrastructure.Persistence.EFCore.Interceptors;
using SampleMicroserviceApp.Identity.Infrastructure.Persistence.EFCore.Repository;
using SampleMicroserviceApp.Identity.Infrastructure.Persistence.EFCore.UnitOfWork;

namespace SampleMicroserviceApp.Identity.Infrastructure.ConfigureServices;

public class DataAccessDiInstaller : IDiInstaller
{
    public void InstallServices(IServiceCollection services, AppSettings appSettings)
    {
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseSqlServer(appSettings.ConnStrSettings!.AppDbConnStr, sqlServerOptions =>
            {
                sqlServerOptions.CommandTimeout((int)TimeSpan.FromMinutes(1)
                    .TotalSeconds); //Default is 30 seconds
            });

            // Interceptors

            // High-level Interceptors
            options.AddInterceptors(new EntityCreateAndModifyInterceptor());

            if (appSettings.MongoDbSettings!.IsEnabled)
            {
                options.AddInterceptors(new AuditSaveChangesInterceptor());
            }

            // Low-level Interceptors
            options.AddInterceptors(new FixSchemaInterceptor());

            if (appSettings.MongoDbSettings!.IsEnabled)
            {
                options.AddInterceptors(new AuditCommandInterceptor
                {
                    LogParameterValues = true,
                    IncludeReaderResults = false,
                    AuditEventType = "EFCommand:{database}:{method}"
                });
            }

            // Show Detailed Errors
            if (appSettings.IsDevelopment)
                options.EnableSensitiveDataLogging().EnableDetailedErrors();
        });
    }

}