using AutoMapper;
using AutoMapper.QueryableExtensions;
using SampleMicroserviceApp.Identity.Application.Common.Contracts;
using SampleMicroserviceApp.Identity.Application.CQRS.User.DTOs;
using SampleMicroserviceApp.Identity.Domain.Entities.User;

namespace SampleMicroserviceApp.Identity.Application.CQRS.User.Queries.GetAllUsers;

public record GetAllUsersQuery : IRequest<IQueryable<UserDto>>;

// Handler
public class GetAllUsersQueryHandler(IRepository<UserEntity> userRepository, IMapper mapper) : IRequestHandler<GetAllUsersQuery, IQueryable<UserDto>>
{
    public Task<IQueryable<UserDto>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        var queryable = userRepository.AsQueryable()
            .ProjectTo<UserDto>(mapper.ConfigurationProvider);

        return Task.FromResult(queryable);
    }
}