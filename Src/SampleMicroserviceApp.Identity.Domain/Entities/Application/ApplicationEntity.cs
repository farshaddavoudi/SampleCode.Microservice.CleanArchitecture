using SampleMicroserviceApp.Identity.Domain.Entities.Claim;
using SampleMicroserviceApp.Identity.Domain.Entities.Role;
using SampleMicroserviceApp.Identity.Domain.Enums.Application;

namespace SampleMicroserviceApp.Identity.Domain.Entities.Application;

public class ApplicationEntity : BaseEntity
{
    public string? Key { get; set; }

    public string? Title { get; set; }

    public AppType AppType { get; set; }

    public bool IsPublic { get; set; }

    public bool IsActive { get; set; }

    public string? IconUrl { get; set; }

    public string? BaseAddress { get; set; }

    public List<int>? RelatedApps { get; set; }

    public string? Description { get; set; }

    //NAVs
    public ICollection<ClaimEntity> Claims { get; set; } = new List<ClaimEntity>();
    public ICollection<RoleEntity> Roles { get; set; } = new List<RoleEntity>();
}