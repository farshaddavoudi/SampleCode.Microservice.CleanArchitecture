using SampleMicroserviceApp.Identity.Domain.Entities.Claim;

namespace SampleMicroserviceApp.Identity.Domain.Entities.Role;

public class RoleClaimEntity : BaseEntity
{
    public int RoleId { get; set; }

    public int ClaimId { get; set; }

    // NAVs
    public RoleEntity? Role { get; set; }
    public ClaimEntity? Claim { get; set; }
}