using Ardalis.Specification;
using SampleMicroserviceApp.Identity.Domain.Entities.Role;

namespace SampleMicroserviceApp.Identity.Application.CQRS.Role.Specifications;

public sealed class UserRoleKeysInAppByAppIdAndUserId : Specification<RoleEntity, string>
{
    public UserRoleKeysInAppByAppIdAndUserId(int appId, int userId)
    {
        Query.Where(r => r.ApplicationId == appId && r.UserRoles.Any(ur => ur.UserId == userId));

        Query!.Select(r => r.Key);
    }
}

