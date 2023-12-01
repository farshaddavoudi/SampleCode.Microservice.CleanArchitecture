namespace SampleMicroserviceApp.Identity.Domain.ConfigurationSettings;

public record ConnStrSettings(string? AppDbConnStr, string? HangfireConnStr);