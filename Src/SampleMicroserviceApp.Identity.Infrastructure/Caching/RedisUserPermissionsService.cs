using SampleMicroserviceApp.Identity.Application.Common.Contracts;
using SampleMicroserviceApp.Identity.Application.Common.Exceptions;
using SampleMicroserviceApp.Identity.Domain.Constants;
using System.Security.Claims;

namespace SampleMicroserviceApp.Identity.Infrastructure.Caching;

public class RedisUserPermissionsService(
    IApplicationCacheService applicationCacheService,
    IUserRolesCacheService userRolesCacheService,
    IUserCacheService userCacheService,
    IRoleClaimsCacheService roleClaimsCacheService
    ) : IUserPermissionsService
{
    public async Task<bool> HasUserAccessToResource(int userId, string appKey, List<string> allowedClaims, List<string> allowedRoles, CancellationToken cancellationToken)
    {
        var userRoles = await GetUserRolesAsync(userId, appKey, cancellationToken);

        if (allowedRoles.Any())
        {
            if (userRoles.Any(allowedRoles.Contains) is false)
            {
                return false;
            }
        }

        var appFromCache = await applicationCacheService.GetAppAsync(appKey, CancellationToken.None);

        if (appFromCache is null)
        {
            return false;
        }

        // If user do not have any roles, so it means he/she does not access to the app
        if (appFromCache.Value.MainApp.IsPublic is false && userRoles.Count == 0)
        {
            return false;
        }

        if (allowedClaims.Any())
        {
            var userAppClaims = await GetUserAppClaimsAsync(userId, appKey, CancellationToken.None);

            if (userAppClaims.Any(allowedClaims.Contains) is false)
            {
                return false;
            }
        }

        return true;
    }

    public async Task<List<string>> GetUserRolesAsync(int userId, string appKey, CancellationToken cancellationToken)
    {
        var appCached = await applicationCacheService.GetAppAsync(appKey, cancellationToken);

        if (appCached is null)
        {
            throw new BadRequestException("AppKey is invalid. No app was found");
        }

        var appKeys = appCached.Value.RelatedApps.Select(x => x.Key!).ToList();

        appKeys.Add(appCached.Value.MainApp.Key!);

        var userRoles = await userRolesCacheService.GetUserRolesAsync(userId, appKeys, cancellationToken);

        return userRoles.Distinct().ToList();
    }

    public async Task<List<Claim>> GetUserIdentityClaimsExceptAppClaimsAsync(int userId, string appKey,
        CancellationToken cancellationToken)
    {
        List<string> appAndRelatedAppsAggregatedRoles = await GetUserRolesAsync(userId, appKey, cancellationToken);

        var userInfo = await userCacheService.GetUserAsync(userId, cancellationToken);

        if (userInfo is null)
            throw new BadRequestException("UserId is incorrect. No user was found");

        List<Claim> claims = new()
        {
            new(ClaimTypes.NameIdentifier, userId.ToString()),
            new(JwtTokenClaimConst.BoxId, userInfo.BoxId?.ToString() ?? ""),
            new(JwtTokenClaimConst.PersonnelCode, userInfo.PersonnelCode?.ToString() ?? ""),
            new(JwtTokenClaimConst.PostTitle, userInfo.PostTitle ?? ""),
            new(JwtTokenClaimConst.UnitName, userInfo.UnitName ?? ""),
            new(JwtTokenClaimConst.WorkLocationCode, userInfo.WorkLocationCode.ToString() ?? "")
        };

        claims.AddRange(appAndRelatedAppsAggregatedRoles.Select(role => new Claim(ClaimTypes.Role, role)));

        return claims;
    }

    public async Task<List<string>> GetUserAppClaimsAsync(int userId, string appKey, CancellationToken cancellationToken)
    {
        var appAndRelatedAppsAggregatedRoles = await GetUserRolesAsync(userId, appKey, cancellationToken);

        List<string> aggregatedClaims = new();

        foreach (var roleKey in appAndRelatedAppsAggregatedRoles)
        {
            var claims = await roleClaimsCacheService.GetRoleClaimsAsync(roleKey, cancellationToken);

            aggregatedClaims.AddRange(claims);
        }

        return aggregatedClaims.Distinct().ToList();
    }
}