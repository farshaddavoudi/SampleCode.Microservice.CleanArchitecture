using SampleMicroserviceApp.Identity.Application.Common.Contracts;
using SampleMicroserviceApp.Identity.Application.CQRS.Application.DTOs;
using SampleMicroserviceApp.Identity.Application.CQRS.Application.Specifications;
using SampleMicroserviceApp.Identity.Domain.Entities.Application;

namespace SampleMicroserviceApp.Identity.Application.CQRS.Application.Queries.GetAllApplications;

public record GetAllApplicationsQuery : IRequest<List<ApplicationDto>>;

// Handler
public class GetAllApplicationsQueryHandler(IReadOnlyRepository<ApplicationEntity> applicationRepository, IApplicationCacheService appCacheService)
    : IRequestHandler<GetAllApplicationsQuery, List<ApplicationDto>>
{
    public async Task<List<ApplicationDto>> Handle(GetAllApplicationsQuery request, CancellationToken cancellationToken)
    {
        var apps = await applicationRepository.ToListProjectedAsync<ApplicationDto>(
                new ApplicationByFiltersSpec(null), cancellationToken);

        foreach (var app in apps)
        {
            if (app.RelatedApps?.Any() ?? false)
            {
                Dictionary<int, string> relatedApplications = new();

                foreach (var relatedAppId in app.RelatedApps)
                {
                    var relatedApp = await appCacheService.GetAppAsync(relatedAppId, cancellationToken);

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