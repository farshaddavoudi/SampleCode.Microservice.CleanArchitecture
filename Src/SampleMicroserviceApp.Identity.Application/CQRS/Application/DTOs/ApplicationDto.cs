using SampleMicroserviceApp.Identity.Domain.Enums.Application;

namespace SampleMicroserviceApp.Identity.Application.CQRS.Application.DTOs;

public class ApplicationDto
{
    public int Id { get; set; }

    public string? Key { get; set; }

    public string? Title { get; set; }

    public AppType AppType { get; set; }
    public string? AppTypeDisplay { get; set; } //Mapper

    public bool IsActive { get; set; }

    public string? IconUrl { get; set; }

    public string? BaseAddress { get; set; }

    public List<int>? RelatedApps { get; set; }

    public Dictionary<int, string> RelatedApplications { get; set; } = new();

    public string? Description { get; set; }
}