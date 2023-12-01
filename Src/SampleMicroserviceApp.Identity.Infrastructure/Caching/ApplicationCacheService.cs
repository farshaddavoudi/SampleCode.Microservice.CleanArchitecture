using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;
using SampleMicroserviceApp.Identity.Application.Common.Contracts;
using SampleMicroserviceApp.Identity.Application.CQRS.Application.Specifications;
using SampleMicroserviceApp.Identity.Domain.Constants;
using SampleMicroserviceApp.Identity.Domain.Entities.Application;

namespace SampleMicroserviceApp.Identity.Infrastructure.Caching;

public class ApplicationCacheService : IApplicationCacheService
{
    private readonly IDistributedCache _distributedCache;
    private readonly IRepository<ApplicationEntity> _appRepository;

    #region ctor

    public ApplicationCacheService(IDistributedCache distributedCache, IRepository<ApplicationEntity> appRepository)
    {
        _distributedCache = distributedCache;
        _appRepository = appRepository;
    }

    #endregion

    public async Task SetAppAsync(ApplicationEntity app, CancellationToken cancellationToken)
    {
        await _distributedCache.SetStringAsync(CacheKeyConst.Application(app.Key!),
            JsonSerializer.Serialize(app),
            cancellationToken);

        await _distributedCache.SetStringAsync(CacheKeyConst.Application(app.Id),
            JsonSerializer.Serialize(app),
            cancellationToken);
    }

    public async Task<(ApplicationEntity MainApp, List<ApplicationEntity> RelatedApps)?> GetAppAsync(string appKey,
        CancellationToken cancellationToken)
    {
        var appSerialized = await _distributedCache.GetStringAsync(CacheKeyConst.Application(appKey), cancellationToken);

        ApplicationEntity? appEntity;

        if (appSerialized is null)
        {
            appEntity = await _appRepository.FirstOrDefaultAsync(
                    new ApplicationByKeySpec(appKey), cancellationToken);

            if (appEntity is null)
                return null;

            await SetAppAsync(appEntity, cancellationToken);
        }
        else
        {
            appEntity = JsonSerializer.Deserialize<ApplicationEntity>(appSerialized);
        }

        List<ApplicationEntity> relatedApps = new();

        foreach (var relatedAppId in appEntity?.RelatedApps ?? new())
        {
            var relatedAppSerialized =
                await _distributedCache.GetStringAsync(CacheKeyConst.Application(relatedAppId),
                    token: cancellationToken);

            ApplicationEntity? relatedAppEntity;

            if (relatedAppSerialized is null)
            {
                relatedAppEntity = await _appRepository.GetByIdAsync(relatedAppId, cancellationToken);

                if (relatedAppEntity is null)
                    continue;

                await SetAppAsync(relatedAppEntity, cancellationToken);
            }
            else
            {
                relatedAppEntity = JsonSerializer.Deserialize<ApplicationEntity>(relatedAppSerialized);
            }

            relatedApps.Add(relatedAppEntity!);
        }

        return (appEntity, relatedApps);
    }

    public async Task<ApplicationEntity?> GetAppAsync(int appId, CancellationToken cancellationToken)
    {
        var appSerialized = await _distributedCache.GetStringAsync(CacheKeyConst.Application(appId), cancellationToken);

        ApplicationEntity? appEntity;

        if (appSerialized is null)
        {
            appEntity = await _appRepository.FirstOrDefaultAsync(
                    new ApplicationByIdSpec(appId), cancellationToken);

            if (appEntity is null)
                return null;

            await SetAppAsync(appEntity, cancellationToken);
        }
        else
        {
            appEntity = JsonSerializer.Deserialize<ApplicationEntity>(appSerialized);
        }

        return appEntity;
    }
}