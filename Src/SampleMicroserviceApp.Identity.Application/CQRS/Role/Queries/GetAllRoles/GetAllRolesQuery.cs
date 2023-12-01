using AutoMapper;
using AutoMapper.QueryableExtensions;
using SampleMicroserviceApp.Identity.Application.Common.Contracts;
using SampleMicroserviceApp.Identity.Application.CQRS.Application.DTOs;
using SampleMicroserviceApp.Identity.Domain.Entities.Role;

namespace SampleMicroserviceApp.Identity.Application.CQRS.Role.Queries.GetAllRoles;

public record GetAllRolesQuery : IRequest<IQueryable<RoleDto>>;

// Handler
public class GetAllRolesQueryHandler : IRequestHandler<GetAllRolesQuery, IQueryable<RoleDto>>
{
    private readonly IMapper _mapper;
    private readonly IRepository<RoleEntity> _roleRepository;

    #region ctor

    public GetAllRolesQueryHandler(IMapper mapper, IRepository<RoleEntity> roleRepository)
    {
        _mapper = mapper;
        _roleRepository = roleRepository;
    }

    #endregion

    public Task<IQueryable<RoleDto>> Handle(GetAllRolesQuery request, CancellationToken cancellationToken)
    {
        var queryable = _roleRepository.AsQueryable()
            .ProjectTo<RoleDto>(_mapper.ConfigurationProvider);

        return Task.FromResult(queryable);
    }
}