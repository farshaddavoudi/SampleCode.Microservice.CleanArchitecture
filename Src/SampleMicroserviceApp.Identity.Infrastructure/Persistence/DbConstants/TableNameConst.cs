namespace SampleMicroserviceApp.Identity.Infrastructure.Persistence.DbConstants;

public class TableNameConst
{
    // Application
    public const string Applications = nameof(Applications);

    // Claim
    public const string Claims = nameof(Claims);

    // Role
    public const string Roles = nameof(Roles);
    public const string RoleClaims = nameof(RoleClaims);

    // User
    public const string Users = nameof(Users);
    public const string UserRoles = nameof(UserRoles);
    public const string RefreshTokensHistory = nameof(RefreshTokensHistory);
}