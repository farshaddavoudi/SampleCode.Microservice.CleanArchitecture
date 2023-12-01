using Ardalis.Specification;
using SampleMicroserviceApp.Identity.Domain.Entities.User;

namespace SampleMicroserviceApp.Identity.Application.CQRS.User.Specifications;

public sealed class UserByIdSpec : Specification<UserEntity>
{
    public UserByIdSpec(int userId)
    {
        Query.Where(u => u.Id == userId);
    }
}