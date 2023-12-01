using SampleMicroserviceApp.Identity.Domain.Entities.Application;
using SampleMicroserviceApp.Identity.Domain.Entities.User;

namespace SampleMicroserviceApp.Identity.Domain.Entities.Role;

public class RoleEntity : BaseEntity
{
    public int ApplicationId { get; set; }

    public string? Key { get; set; }

    public string? Title { get; set; }

    // NAVs
    public ApplicationEntity? Application { get; set; }
    public ICollection<RoleClaimEntity> RoleClaims { get; set; } = new List<RoleClaimEntity>();
    public ICollection<UserRoleEntity> UserRoles { get; set; } = new List<UserRoleEntity>();
}