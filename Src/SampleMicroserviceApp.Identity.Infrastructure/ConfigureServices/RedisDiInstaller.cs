using StackExchange.Redis;
using SampleMicroserviceApp.Identity.Application.Common.Contracts;
using SampleMicroserviceApp.Identity.Domain.ConfigurationSettings;

namespace SampleMicroserviceApp.Identity.Infrastructure.ConfigureServices;

public class RedisDiInstaller : IDiInstaller
{
    public void InstallServices(IServiceCollection services, AppSettings appSettings)
    {
        services.AddStackExchangeRedisCache(redisOptions =>
        {
            redisOptions.Configuration = appSettings.RedisSettings!.ConnectionString;
        });

        services.AddSingleton<IConnectionMultiplexer>(opt =>
            ConnectionMultiplexer.Connect(appSettings.RedisSettings!.ConnectionString));
    }
}