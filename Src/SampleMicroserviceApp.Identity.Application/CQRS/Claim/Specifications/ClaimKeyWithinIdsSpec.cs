using Ardalis.Specification;
using SampleMicroserviceApp.Identity.Domain.Entities.Claim;

namespace SampleMicroserviceApp.Identity.Application.CQRS.Claim.Specifications;

public sealed class ClaimKeyWithinIdsSpec : Specification<ClaimEntity, string>
{
    public ClaimKeyWithinIdsSpec(IEnumerable<int> claimIds)
    {
        Query.Where(c => claimIds.Contains(c.Id));

        Query!.Select(c => c.Key);
    }
}