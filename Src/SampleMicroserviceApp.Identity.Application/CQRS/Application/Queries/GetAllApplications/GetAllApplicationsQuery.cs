using SampleMicroserviceApp.Identity.Application.Common.Contracts;
using SampleMicroserviceApp.Identity.Application.CQRS.Application.DTOs;
using SampleMicroserviceApp.Identity.Application.CQRS.Application.Specifications;
using SampleMicroserviceApp.Identity.Domain.Entities.Application;

namespace SampleMicroserviceApp.Identity.Application.CQRS.Application.Queries.GetAllApplications;

public record GetAllApplicationsQuery : IRequest<List<ApplicationDto>>;

// Handler
public class GetAllApplicationsQueryHandler : IRequestHandler<GetAllApplicationsQuery, List<ApplicationDto>>
{
    private readonly IRepository<ApplicationEntity> _applicationRepository;
    private readonly IApplicationCacheService _appCacheService;

    #region ctor

    public GetAllApplicationsQueryHandler(IRepository<ApplicationEntity> applicationRepository, IApplicationCacheService applicationCacheService)
    {
        _applicationRepository = applicationRepository;
        _appCacheService = applicationCacheService;
    }

    #endregion

    public async Task<List<ApplicationDto>> Handle(GetAllApplicationsQuery request, CancellationToken cancellationToken)
    {
        var apps = await _applicationRepository.ToListProjectedAsync<ApplicationDto>(
                new ApplicationByFiltersSpec(null), cancellationToken);

        foreach (var app in apps)
        {
            if (app.RelatedApps?.Any() ?? false)
            {
                Dictionary<int, string> relatedApplications = new();

                foreach (var relatedAppId in app.RelatedApps)
                {
                    var relatedApp = await _appCacheService.GetAppAsync(relatedAppId, cancellationToken);

                    if (relatedApp is not null)
                    {
                        relatedApplications.Add(relatedAppId, relatedApp.Title!);
                    }
                }

                app.RelatedApplications = relatedApplications;
            }
        }

        return apps;
    }
}