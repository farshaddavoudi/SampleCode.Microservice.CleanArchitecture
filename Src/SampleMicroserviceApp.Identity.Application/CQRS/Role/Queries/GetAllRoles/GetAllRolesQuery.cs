using AutoMapper;
using AutoMapper.QueryableExtensions;
using SampleMicroserviceApp.Identity.Application.Common.Contracts;
using SampleMicroserviceApp.Identity.Application.CQRS.Application.DTOs;
using SampleMicroserviceApp.Identity.Domain.Entities.Role;

namespace SampleMicroserviceApp.Identity.Application.CQRS.Role.Queries.GetAllRoles;

public record GetAllRolesQuery : IRequest<IQueryable<RoleDto>>;

// Handler
public class GetAllRolesQueryHandler(IMapper mapper, IReadOnlyRepository<RoleEntity> roleRepository) : IRequestHandler<GetAllRolesQuery, IQueryable<RoleDto>>
{
    public Task<IQueryable<RoleDto>> Handle(GetAllRolesQuery request, CancellationToken cancellationToken)
    {
        var queryable = roleRepository.AsQueryable()
            .ProjectTo<RoleDto>(mapper.ConfigurationProvider);

        return Task.FromResult(queryable);
    }
}