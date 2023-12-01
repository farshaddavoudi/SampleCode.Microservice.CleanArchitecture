using Ardalis.Specification;
using SampleMicroserviceApp.Identity.Domain.Entities.User;

namespace SampleMicroserviceApp.Identity.Application.CQRS.Role.Specifications;

public sealed class UserRolePairsByRoleIdSpec : Specification<UserRoleEntity>
{
    public UserRolePairsByRoleIdSpec(int roleId)
    {
        Query.Where(ur => ur.RoleId == roleId);
    }
}