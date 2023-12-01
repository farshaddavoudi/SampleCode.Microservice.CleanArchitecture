using Ardalis.Specification;
using SampleMicroserviceApp.Identity.Domain.Entities.Role;

namespace SampleMicroserviceApp.Identity.Application.CQRS.Role.Specifications;

public sealed class ApplicationIdAndKeyByRoleIdSpec : Specification<RoleEntity, Tuple<int, string>> //AppId, AppKey
{
    public ApplicationIdAndKeyByRoleIdSpec(int roleId)
    {
        Query.Where(r => r.Id == roleId);

        Query.Select(r => new Tuple<int, string>(r.ApplicationId, r.Application!.Key!));
    }
}