using SampleMicroserviceApp.Identity.Domain.Entities.Application;

namespace SampleMicroserviceApp.Identity.Application.Common.Contracts;

public interface IApplicationCacheService
{
    Task SetAppAsync(ApplicationEntity app, CancellationToken cancellationToken);

    Task<(ApplicationEntity MainApp, List<ApplicationEntity> RelatedApps)?> GetAppAsync(string appKey, CancellationToken cancellationToken);

    Task<ApplicationEntity?> GetAppAsync(int appId, CancellationToken cancellationToken);
}