// ReSharper disable InconsistentNaming
using System.Reflection;

namespace SampleMicroserviceApp.Identity.Domain.Constants;

public class ClaimConst //* Do not change Claim values *//
{
    // User
    public const string Identity_User_ViewAll = nameof(Identity_User_ViewAll);
    public const string Identity_User_Manage = nameof(Identity_User_Manage);
    // Role
    public const string Identity_Role_ViewAll = nameof(Identity_Role_ViewAll);
    public const string Identity_Role_Manage = nameof(Identity_Role_Manage);
    // Application
    public const string Identity_Application_ViewAll = nameof(Identity_Application_ViewAll);
    public const string Identity_Application_Manage = nameof(Identity_Application_Manage);
    // Permission
    public const string Identity_Permission_LoginAsSomeoneElse = nameof(Identity_Permission_LoginAsSomeoneElse);

    private static string[]? _claimNames;

    public static IEnumerable<string> GetAllAppClaims()
    {
        return _claimNames ??= typeof(ClaimConst)
            .GetFields(BindingFlags.Public | BindingFlags.Static)
            .Select(field => (string?)field.GetValue(null))
            .ToArray();
    }
}