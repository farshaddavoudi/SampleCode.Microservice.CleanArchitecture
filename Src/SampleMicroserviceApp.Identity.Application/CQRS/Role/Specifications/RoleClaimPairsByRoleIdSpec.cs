using Ardalis.Specification;
using SampleMicroserviceApp.Identity.Domain.Entities.Role;

namespace SampleMicroserviceApp.Identity.Application.CQRS.Role.Specifications;

public sealed class RoleClaimPairsByRoleIdSpec : Specification<RoleClaimEntity>
{
    public RoleClaimPairsByRoleIdSpec(int roleId)
    {
        Query.Where(rc => rc.RoleId == roleId);
    }
}