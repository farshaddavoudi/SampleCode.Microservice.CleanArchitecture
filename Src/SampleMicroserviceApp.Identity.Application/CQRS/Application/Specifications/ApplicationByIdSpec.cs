using Ardalis.Specification;
using SampleMicroserviceApp.Identity.Domain.Entities.Application;

namespace SampleMicroserviceApp.Identity.Application.CQRS.Application.Specifications;

public sealed class ApplicationByIdSpec : Specification<ApplicationEntity>
{
    public ApplicationByIdSpec(int appId)
    {
        Query.Where(a => a.Id == appId);
    }
}