using Grpc.Core;
using SampleMicroserviceApp.Identity.Application.Common.Contracts;

namespace SampleMicroserviceApp.Identity.Web.API.Grpc;

public class PermissionsGrpcService : UserPermissions.UserPermissionsBase
{
    private readonly IUserPermissionsService _userPermissionsService;

    #region ctor

    public PermissionsGrpcService(IUserPermissionsService userPermissionsService)
    {
        _userPermissionsService = userPermissionsService;
    }

    #endregion

    public override async Task<HasUserAccessToResourceResponse> HasUserAccessToResource(HasUserAccessToResourceRequest request, ServerCallContext context)
    {
        if (string.IsNullOrWhiteSpace(request.AppKey))
            throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid ApiKey"));

        bool hasAccess = await _userPermissionsService.HasUserAccessToResource(
        request.UserId, request.AppKey, request.AllowedClaims.ToList(), request.AllowedRoles.ToList(), CancellationToken.None);

        return new HasUserAccessToResourceResponse { HasAccess = hasAccess };
    }

    public override async Task<GetUserRolesResponse> GetUserRoles(GetUserRolesRequest request, ServerCallContext context)
    {
        var roles = await _userPermissionsService.GetUserRolesAsync(request.UserId, request.AppKey, CancellationToken.None);

        return new GetUserRolesResponse
        {
            Roles = { roles }
        };
    }
}