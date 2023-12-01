﻿using System.Security.Claims;
using SampleMicroserviceApp.Identity.Application.Common.Contracts;
using SampleMicroserviceApp.Identity.Domain.Shared;

namespace SampleMicroserviceApp.Identity.Web.API.Middlewares;

public class ClaimsMiddleware : IMiddleware
{
    private readonly IUserPermissionsService _userPermissionsService;
    private readonly ICurrentUserService _currentUserService;

    #region ctor

    public ClaimsMiddleware(IUserPermissionsService userPermissionsService, ICurrentUserService currentUserService)
    {
        _userPermissionsService = userPermissionsService;
        _currentUserService = currentUserService;
    }

    #endregion

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        if (_currentUserService.IsAuthenticated())
        {
            // Attach retrieved claims to the current user

            var claimsIdentity = (ClaimsIdentity)context.User.Identity!;

            var userIdEncrypted = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var userClaims = await _userPermissionsService.GetUserIdentityClaimsExceptAppClaimsAsync(_currentUserService.User()!.Id, AppMetadataConst.AppKey, CancellationToken.None);

            // Remove the encrypted UserId which will replace with actual integer UserId due to using of some tools such as SignalR and client

            claimsIdentity.TryRemoveClaim(new Claim(ClaimTypes.NameIdentifier, userIdEncrypted!));

            claimsIdentity.AddClaims(userClaims);
        }

        await next(context);
    }
}