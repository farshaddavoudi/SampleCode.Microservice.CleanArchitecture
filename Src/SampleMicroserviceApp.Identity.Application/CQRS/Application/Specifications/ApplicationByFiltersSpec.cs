using Ardalis.Specification;
using SampleMicroserviceApp.Identity.Domain.Entities.Application;

namespace SampleMicroserviceApp.Identity.Application.CQRS.Application.Specifications;

public class ApplicationByFiltersSpec : Specification<ApplicationEntity>
{
    public ApplicationByFiltersSpec(
        string? searchTerm
        )
    {
        //Query
    }
}