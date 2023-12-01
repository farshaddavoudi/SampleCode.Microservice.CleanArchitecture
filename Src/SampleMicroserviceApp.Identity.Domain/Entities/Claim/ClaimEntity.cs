using SampleMicroserviceApp.Identity.Domain.Entities.Application;
using SampleMicroserviceApp.Identity.Domain.Entities.Role;

namespace SampleMicroserviceApp.Identity.Domain.Entities.Claim;

public class ClaimEntity : BaseEntity
{
    public int ApplicationId { get; set; }

    public string? Key { get; set; }

    public string? Title { get; set; }

    // NAVs
    public ApplicationEntity? Application { get; set; }
    public ICollection<RoleClaimEntity> RoleClaims { get; set; } = new List<RoleClaimEntity>();
}