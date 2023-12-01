using Ardalis.Specification;
using SampleMicroserviceApp.Identity.Domain.Entities.User;

namespace SampleMicroserviceApp.Identity.Application.CQRS.User.Specifications;

public sealed class UserByUsernameSpec : Specification<UserEntity>
{
    public UserByUsernameSpec(string username)
    {
        Query.Where(u => u.UserName == username);
    }
}