using Ardalis.Specification;
using SampleMicroserviceApp.Identity.Domain.Entities.Claim;

namespace SampleMicroserviceApp.Identity.Application.CQRS.Claim.Specifications;

public sealed class ClaimByKeySpec : Specification<ClaimEntity>
{
    public ClaimByKeySpec(string claimKey)
    {
        Query.Where(c => c.Key == claimKey);
    }
}