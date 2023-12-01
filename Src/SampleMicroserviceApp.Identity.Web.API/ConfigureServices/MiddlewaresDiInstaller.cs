using SampleMicroserviceApp.Identity.Application.Common.Contracts;
using SampleMicroserviceApp.Identity.Domain.ConfigurationSettings;
using SampleMicroserviceApp.Identity.Web.API.Middlewares;

namespace SampleMicroserviceApp.Identity.Web.API.ConfigureServices;

public class MiddlewaresDiInstaller : IDiInstaller
{
    public void InstallServices(IServiceCollection services, AppSettings appSettings)
    {
        services.AddTransient<GlobalExceptionHandlingMiddleware>();

        services.AddTransient<CorrelationIdHandlingMiddleware>();

        services.AddTransient<ClaimsMiddleware>();

        services.AddTransient<RefreshTokenMiddleware>();

        services.AddTransient<HandleHangfireDashboardAuthMiddleware>();

        // Serilog
        services.AddTransient<AddCorrelationIdToSerilogContextMiddleware>();
        services.AddTransient<AddCurrentUserToSerilogContextMiddleware>();
        services.AddTransient<AddClientIpToSerilogContextMiddleware>();

        // Audit.NET
        services.AddTransient<AddCorrelationIdToAuditNetLogsMiddleware>();
        services.AddTransient<AddCurrentUserToAuditNetLogsMiddleware>();
        services.AddTransient<AddClientIpToAuditNetLogsMiddleware>();
    }
}