using AutoMapper;
using AutoMapper.QueryableExtensions;
using SampleMicroserviceApp.Identity.Application.Common.Contracts;
using SampleMicroserviceApp.Identity.Application.CQRS.Application.DTOs;
using SampleMicroserviceApp.Identity.Domain.Entities.Claim;

namespace SampleMicroserviceApp.Identity.Application.CQRS.Claim.Queries.GetAllClaims;

public record GetAllClaimsQuery : IRequest<IQueryable<ClaimDto>>;

// Handler
public class GetAllClaimsQueryHandler(IMapper mapper, IReadOnlyRepository<ClaimEntity> claimRepository)
    : IRequestHandler<GetAllClaimsQuery, IQueryable<ClaimDto>>
{
    public Task<IQueryable<ClaimDto>> Handle(GetAllClaimsQuery request, CancellationToken cancellationToken)
    {
        var queryable = claimRepository.AsQueryable()
            .ProjectTo<ClaimDto>(mapper.ConfigurationProvider);

        return Task.FromResult(queryable);
    }
}