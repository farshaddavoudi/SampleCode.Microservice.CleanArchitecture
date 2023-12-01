using AutoMapper;
using AutoMapper.QueryableExtensions;
using SampleMicroserviceApp.Identity.Application.Common.Contracts;
using SampleMicroserviceApp.Identity.Application.CQRS.Application.DTOs;
using SampleMicroserviceApp.Identity.Domain.Entities.Claim;

namespace SampleMicroserviceApp.Identity.Application.CQRS.Claim.Queries.GetAllClaims;

public record GetAllClaimsQuery : IRequest<IQueryable<ClaimDto>>;

// Handler
public class GetAllClaimsQueryHandler : IRequestHandler<GetAllClaimsQuery, IQueryable<ClaimDto>>
{
    private readonly IMapper _mapper;
    private readonly IRepository<ClaimEntity> _claimRepository;

    #region ctor

    public GetAllClaimsQueryHandler(IMapper mapper, IRepository<ClaimEntity> claimRepository)
    {
        _mapper = mapper;
        _claimRepository = claimRepository;
    }

    #endregion

    public Task<IQueryable<ClaimDto>> Handle(GetAllClaimsQuery request, CancellationToken cancellationToken)
    {
        var queryable = _claimRepository.AsQueryable()
            .ProjectTo<ClaimDto>(_mapper.ConfigurationProvider);

        return Task.FromResult(queryable);
    }
}