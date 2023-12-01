namespace SampleMicroserviceApp.Identity.Domain.Constants;

public static class CacheKeyConst
{
    public static string User(int userId) => $"Identity:Users:UserId={userId}";
    public static string UserRefreshToken(int userIdAsKey) => $"Identity:RefreshTokens:UserId={userIdAsKey}";
    public static string UserRefreshToken(string refreshTokenAsKey) => $"Identity:RefreshTokens:RefreshToken={refreshTokenAsKey}";
    public static string RoleClaims(string roleKey) => $"Identity:RoleClaims:Role={roleKey}";
    public static class UserRolesHash
    {
        public static string UserRolesKey(int userId) => $"Identity:UserRoles:UserId={userId}";
        public static string UserRolesHashField(string appKey) => appKey;
    }
    public static string Application(string appKey) => $"Identity:Applications:App={appKey}";
    public static string Application(int appId) => $"Identity:Applications:AppId={appId}";
}