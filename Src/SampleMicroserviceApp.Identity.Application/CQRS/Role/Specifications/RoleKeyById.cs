using Ardalis.Specification;
using SampleMicroserviceApp.Identity.Domain.Entities.Role;

namespace SampleMicroserviceApp.Identity.Application.CQRS.Role.Specifications;

public sealed class RoleKeyById : Specification<RoleEntity, string>
{
    public RoleKeyById(int roleId)
    {
        Query.Where(r => r.Id == roleId);

        Query!.Select(r => r.Key);
    }
}