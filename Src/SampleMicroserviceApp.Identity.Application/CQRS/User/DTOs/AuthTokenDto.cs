namespace SampleMicroserviceApp.Identity.Application.CQRS.User.DTOs;

public class AuthTokenDto
{
    public string? AccessToken { get; set; }

    public string? RefreshToken { get; set; }

    public DateTime JwtExpiresAt { get; set; }
}