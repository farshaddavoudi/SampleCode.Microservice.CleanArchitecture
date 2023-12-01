namespace SampleMicroserviceApp.Identity.Application.CQRS.Application.DTOs;

public class RoleDto
{
    public int ApplicationId { get; set; }

    public string? ApplicationTitle { get; set; } //Flattening

    public string? Key { get; set; }

    public string? Title { get; set; }

    public List<ClaimDto> Claims { get; set; } = new();
}