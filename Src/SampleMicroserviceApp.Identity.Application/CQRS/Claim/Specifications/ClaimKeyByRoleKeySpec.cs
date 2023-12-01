using Ardalis.Specification;
using SampleMicroserviceApp.Identity.Domain.Entities.Claim;

namespace SampleMicroserviceApp.Identity.Application.CQRS.Claim.Specifications;

public sealed class ClaimKeyByRoleKeySpec : Specification<ClaimEntity, string>
{
    public ClaimKeyByRoleKeySpec(string roleKey)
    {
        Query.Where(c => c.RoleClaims.Any(rc => rc.Role!.Key == roleKey));

        Query!.Select(c => c.Key);
    }
}