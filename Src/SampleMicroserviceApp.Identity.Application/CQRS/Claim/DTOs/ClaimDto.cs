namespace SampleMicroserviceApp.Identity.Application.CQRS.Application.DTOs;

public class ClaimDto
{
    public int ApplicationId { get; set; }

    public string? ApplicationTitle { get; set; } //Flattening

    public string? Key { get; set; }

    public string? Title { get; set; }
}