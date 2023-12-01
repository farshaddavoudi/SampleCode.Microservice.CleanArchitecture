namespace SampleMicroserviceApp.Identity.Application.CQRS.User.Queries.GetUserAccessibleApps;

//// Query
//[ComplexType]
//public record GetUsersHavingAtLeastOneRoleQuery(string? SearchTerm) : IRequest<IQueryable<UserDto>>;

//// Handler
//public class GetUsersHavingAtLeastOneRoleQueryHandler : IRequestHandler<GetUsersHavingAtLeastOneRoleQuery, IQueryable<UserDto>>
//{
//    private readonly IRepository<UserIdentityEntity> _userRepository;
//    private readonly IMapper _mapper;

//    #region ctor

//    public GetUsersHavingAtLeastOneRoleQueryHandler(IRepository<UserIdentityEntity> userRepository, IMapper mapper)
//    {
//        _userRepository = userRepository;
//        _mapper = mapper;
//    }

//    #endregion

//    public Task<IQueryable<UserDto>> Handle(GetUsersHavingAtLeastOneRoleQuery request, CancellationToken cancellationToken)
//    {
//        var searchTerm = request.SearchTerm?.Trim().ToLower();

//        var searchTermIsNumber = searchTerm.IsNotNullOrEmpty() && searchTerm!.IsInt();

//        var hasValidLengthToSearch = searchTerm?.Length > 2;

//        return Task.FromResult(_userRepository.GetAll()
//            .WhereIf(searchTerm.IsNotNullOrEmpty(), u =>
//                searchTermIsNumber && u.UserId == searchTerm!.ToInt()
//                || searchTermIsNumber && u.PersonnelCode == searchTerm!.ToInt()
//                || hasValidLengthToSearch && u.FullName!.ToLower().Contains(searchTerm!)
//                || hasValidLengthToSearch && u.UserRolePairs.Any(ur => ur.RoleName!.Contains(searchTerm!)))
//            .Where(u => u.UserRolePairs.Count > 0)
//            .ProjectTo<UserDto>(_mapper.ConfigurationProvider));
//    }
//}