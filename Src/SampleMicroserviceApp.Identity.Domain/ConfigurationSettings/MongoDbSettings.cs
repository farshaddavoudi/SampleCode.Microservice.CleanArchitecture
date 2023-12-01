namespace SampleMicroserviceApp.Identity.Domain.ConfigurationSettings;

public record MongoDbSettings(
    bool IsEnabled,
    string? ConnectionString,
    string? DatabaseName,
    string? CollectionName
);