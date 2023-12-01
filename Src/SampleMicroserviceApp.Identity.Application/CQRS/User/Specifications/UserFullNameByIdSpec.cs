using Ardalis.Specification;
using SampleMicroserviceApp.Identity.Domain.Entities.User;

namespace SampleMicroserviceApp.Identity.Application.CQRS.User.Specifications;

public sealed class UserFullNameByIdSpec : Specification<UserEntity, string>
{
    public UserFullNameByIdSpec(int userId)
    {
        Query.Where(u => u.Id == userId);

        Query!.Select(u => u.FullName);
    }
}