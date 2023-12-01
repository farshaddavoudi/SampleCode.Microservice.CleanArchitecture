using SampleMicroserviceApp.Identity.Application.Common.Contracts;
using SampleMicroserviceApp.Identity.Application.Common.Exceptions;
using SampleMicroserviceApp.Identity.Domain.Enums.Application;

namespace SampleMicroserviceApp.Identity.Application.CQRS.Application.Queries.GetClientAppUserClaims;

public record GetClientAppUserAllClaimsQuery(string ClientAppKey) : IRequest<List<System.Security.Claims.Claim>>;

// Handler
public class GetClientAppUserAllClaimsQueryHandler : IRequestHandler<GetClientAppUserAllClaimsQuery, List<System.Security.Claims.Claim>>
{
    private readonly ICurrentUserService _currentUserService;
    private readonly IUserPermissionsService _userPermissionsService;
    private readonly IApplicationCacheService _applicationCacheService;

    #region ctor

    public GetClientAppUserAllClaimsQueryHandler(ICurrentUserService currentUserService, IUserPermissionsService userPermissionsService, IApplicationCacheService applicationCacheService)
    {
        _currentUserService = currentUserService;
        _userPermissionsService = userPermissionsService;
        _applicationCacheService = applicationCacheService;
    }

    #endregion

    public async Task<List<System.Security.Claims.Claim>> Handle(GetClientAppUserAllClaimsQuery request, CancellationToken cancellationToken)
    {
        // Check provided AppKey is a Client 
        var app = await _applicationCacheService.GetAppAsync(request.ClientAppKey, cancellationToken);

        if (app?.MainApp is null)
        {
            throw new BadRequestException("AppKey is incorrect");
        }

        if (app.Value.MainApp.AppType is not AppType.FrontEnd or AppType.CombinedFrontAndBack)
        {
            throw new BadRequestException("Only client apps are allowed to call this method");
        }

        List<System.Security.Claims.Claim> claims = new();

        int userId = _currentUserService.User()!.Id;

        // Get UserInfo (Mini) and roles to add to the claims
        var identityClaims = await _userPermissionsService.GetUserIdentityClaimsExceptAppClaimsAsync(userId, request.ClientAppKey, cancellationToken);

        claims.AddRange(identityClaims);

        // Get User App Claims to add to the claims
        var appClaims = await _userPermissionsService.GetUserAppClaimsAsync(userId, request.ClientAppKey, cancellationToken);

        claims.AddRange(appClaims.Select(c => new System.Security.Claims.Claim(c, c)));

        return claims;
    }
}