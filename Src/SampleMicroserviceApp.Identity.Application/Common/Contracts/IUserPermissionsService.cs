using System.Security.Claims;

namespace SampleMicroserviceApp.Identity.Application.Common.Contracts;

public interface IUserPermissionsService
{
    Task<bool> HasUserAccessToResource(int userId, string appKey, List<string> allowedClaims, List<string> allowedRoles, CancellationToken cancellationToken);

    Task<List<string>> GetUserRolesAsync(int userId, string appKey, CancellationToken cancellationToken);

    Task<List<string>> GetUserAppClaimsAsync(int userId, string appKey, CancellationToken cancellationToken);

    Task<List<Claim>> GetUserIdentityClaimsExceptAppClaimsAsync(int userId, string appKey, CancellationToken cancellationToken);
}