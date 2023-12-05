using SampleMicroserviceApp.Identity.Application.Common.Contracts;
using SampleMicroserviceApp.Identity.Application.Common.Exceptions;
using SampleMicroserviceApp.Identity.Domain.Enums.Application;

namespace SampleMicroserviceApp.Identity.Application.CQRS.Application.Queries.GetClientAppUserClaims;

public record GetClientAppUserAllClaimsQuery(string ClientAppKey) : IRequest<List<System.Security.Claims.Claim>>;

// Handler
public class GetClientAppUserAllClaimsQueryHandler(
        ICurrentUserService currentUserService,
        IUserPermissionsService userPermissionsService,
        IApplicationCacheService applicationCacheService
        ) : IRequestHandler<GetClientAppUserAllClaimsQuery, List<System.Security.Claims.Claim>>
{
    public async Task<List<System.Security.Claims.Claim>> Handle(GetClientAppUserAllClaimsQuery request, CancellationToken cancellationToken)
    {
        // Check provided AppKey is a Client 
        var app = await applicationCacheService.GetAppAsync(request.ClientAppKey, cancellationToken);

        if (app?.MainApp is null)
        {
            throw new BadRequestException("AppKey is incorrect");
        }

        if (app.Value.MainApp.AppType is not AppType.FrontEnd or AppType.CombinedFrontAndBack)
        {
            throw new BadRequestException("Only client apps are allowed to call this method");
        }

        List<System.Security.Claims.Claim> claims = new();

        int userId = currentUserService.User()!.Id;

        // Get UserInfo (Mini) and roles to add to the claims
        var identityClaims = await userPermissionsService.GetUserIdentityClaimsExceptAppClaimsAsync(userId, request.ClientAppKey, cancellationToken);

        claims.AddRange(identityClaims);

        // Get User App Claims to add to the claims
        var appClaims = await userPermissionsService.GetUserAppClaimsAsync(userId, request.ClientAppKey, cancellationToken);

        claims.AddRange(appClaims.Select(c => new System.Security.Claims.Claim(c, c)));

        return claims;
    }
}