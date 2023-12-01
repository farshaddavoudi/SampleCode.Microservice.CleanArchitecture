namespace SampleMicroserviceApp.Identity.Domain.Entities.User;

public class RefreshTokenHistoryEntity : BaseEntity
{
    public int UserId { get; set; }

    public string? RefreshToken { get; set; }

    public DateTime ExpiresAt { get; set; }

    public bool IsValid { get; set; }

    // NAVS
    public UserEntity? User { get; set; }
}