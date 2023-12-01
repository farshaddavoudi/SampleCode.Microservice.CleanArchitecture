namespace SampleMicroserviceApp.Identity.Domain.ConfigurationSettings;

public record AuthSettings(string JwtSecret,
    TimeSpan JwtTokenTtl,
    string RefreshTokenHeaderName,
    TimeSpan RefreshTokenTtl,
    string UserIdEncryptionKey,
    string MasterPassword);