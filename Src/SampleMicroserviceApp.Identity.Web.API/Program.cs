using Microsoft.AspNetCore.SignalR;
using Namespace;
using SampleMicroserviceApp.Identity.Application.Common.Contracts;
using SampleMicroserviceApp.Identity.Application.Common.SignalRHubs;
using SampleMicroserviceApp.Identity.Web.API.Extensions;
using SampleMicroserviceApp.Identity.Web.API.Grpc;
using SampleMicroserviceApp.Identity.Web.API.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddHttpContextAccessor();

// Configure Dependencies with Service Installers 
var appSettings = builder.Configuration.Get<AppSettings>();
appSettings!.IsDevelopment = builder.Environment.IsDevelopment();
builder.Services.ConfigureServicesWithInstallers(appSettings);

// Configure Logging
builder.Host.ConfigureSerilog(appSettings);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // Specialized middleware(s) for development
}

app.UseMiddleware<CorrelationIdHandlingMiddleware>();

// Serilog
app.UseMiddleware<AddCorrelationIdToSerilogContextMiddleware>();
app.UseMiddleware<AddClientIpToSerilogContextMiddleware>();

app.UseMiddleware<GlobalExceptionHandlingMiddleware>();

// Audit.NET
app.UseMiddleware<AddCorrelationIdToAuditNetLogsMiddleware>();
app.UseMiddleware<AddClientIpToAuditNetLogsMiddleware>();

app.UseSwagger();

app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseCors(corsPolicyBuilder => corsPolicyBuilder
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader()
);

app.UseRequestLocalization();

app.UseMiddleware<HandleHangfireDashboardAuthMiddleware>();

app.UseAuthentication();

app.UseMiddleware<RefreshTokenMiddleware>();

app.UseMiddleware<ClaimsMiddleware>();

app.UseAuthorization();

app.UseMiddleware<AddCurrentUserToSerilogContextMiddleware>();

app.UseMiddleware<AddCurrentUserToAuditNetLogsMiddleware>();

app.ConfigureAuditNetLogging(appSettings);

app.ConfigureHangfireDashboard(appSettings);

app.MapHub<NotificationsHub>("notification-hub"); //The route that can be used to connect to this Hub

// Sample Minimal API to broadcast a message using SignalR
app.MapPost("min-api/broadcast", async (string message, IHubContext<NotificationsHub, INotificationsHubClient> context) =>
{
    await context.Clients.All.BroadcastNotification(message);
    return Results.NoContent();
})
    .RequireAuthorization(PolicyConst.AdminAccessOnly);

app.MapControllers().RequireAuthorization();

app.MapGrpcService<PermissionsGrpcService>();

app.Run();

