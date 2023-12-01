using SampleMicroserviceApp.Identity.Domain.Entities.Role;

namespace SampleMicroserviceApp.Identity.Domain.Entities.User;

public class UserRoleEntity : BaseEntity
{
    public int UserId { get; set; }

    public int RoleId { get; set; }

    // NAVs 
    public UserEntity? User { get; set; }
    public RoleEntity? Role { get; set; }
}