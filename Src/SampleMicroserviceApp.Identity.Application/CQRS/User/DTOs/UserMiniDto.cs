namespace SampleMicroserviceApp.Identity.Application.CQRS.User.DTOs;

/// <summary>
/// Information can be extracted from JWT token
/// </summary>
public class UserMiniDto
{
    public int Id { get; set; }

    public string? FullName { get; set; }

    public int? PersonnelCode { get; set; }

    public string? UnitName { get; set; }

    public string? PostTitle { get; set; }

    public int? BoxId { get; set; }
    public bool HasBox => BoxId.HasValue;

    public int? WorkLocationCode { get; set; }

    public List<string> Roles { get; set; } = new();
}