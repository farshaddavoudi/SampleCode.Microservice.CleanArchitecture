using SampleMicroserviceApp.Identity.Application.CQRS.User.DTOs;

namespace SampleMicroserviceApp.Identity.Application.Common.Contracts;

public interface ICurrentUserService
{
    bool IsAuthenticated();

    UserMiniDto? User();

    bool IsAdmin();
}