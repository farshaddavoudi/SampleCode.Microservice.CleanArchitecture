using AutoMapper;
using AutoMapper.QueryableExtensions;
using SampleMicroserviceApp.Identity.Application.Common.Contracts;
using SampleMicroserviceApp.Identity.Application.CQRS.User.DTOs;
using SampleMicroserviceApp.Identity.Domain.Entities.User;

namespace SampleMicroserviceApp.Identity.Application.CQRS.User.Queries.GetAllUsers;

public record GetAllUsersQuery : IRequest<IQueryable<UserDto>>;

// Handler
public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, IQueryable<UserDto>>
{
    private readonly IRepository<UserEntity> _userRepository;
    private readonly IMapper _mapper;

    #region ctor

    public GetAllUsersQueryHandler(IRepository<UserEntity> userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    #endregion

    public Task<IQueryable<UserDto>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        var queryable = _userRepository.AsQueryable()
            .ProjectTo<UserDto>(_mapper.ConfigurationProvider);

        return Task.FromResult(queryable);
    }
}