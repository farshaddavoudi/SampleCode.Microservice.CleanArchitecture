using Ardalis.Specification;
using SampleMicroserviceApp.Identity.Domain.Entities.Application;

namespace SampleMicroserviceApp.Identity.Application.CQRS.Application.Specifications;

public sealed class ApplicationByKeySpec : Specification<ApplicationEntity>
{
    public ApplicationByKeySpec(string appKey)
    {
        Query.Where(a => a.Key == appKey);
    }
}