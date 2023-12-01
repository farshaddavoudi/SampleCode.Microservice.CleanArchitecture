using Ardalis.Specification;
using SampleMicroserviceApp.Identity.Domain.Entities.Role;

namespace SampleMicroserviceApp.Identity.Application.CQRS.Role.Specifications;

public sealed class RoleByKeySpec : Specification<RoleEntity>
{
    public RoleByKeySpec(string roleKey)
    {
        Query.Where(r => r.Key == roleKey);
    }
}