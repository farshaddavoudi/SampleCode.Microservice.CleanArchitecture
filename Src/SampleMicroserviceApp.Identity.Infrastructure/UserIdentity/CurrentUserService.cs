using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using SampleMicroserviceApp.Identity.Application.Common.Contracts;
using SampleMicroserviceApp.Identity.Application.CQRS.User.DTOs;
using SampleMicroserviceApp.Identity.Domain.ConfigurationSettings;
using SampleMicroserviceApp.Identity.Domain.Constants;
using SampleMicroserviceApp.Identity.Domain.Extensions;
using SampleMicroserviceApp.Identity.Domain.Utilities;

namespace SampleMicroserviceApp.Identity.Infrastructure.UserIdentity;

public class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly CryptoUtility _cryptoUtility;
    private readonly AppSettings _appSettings;

    #region ctor

    public CurrentUserService(IHttpContextAccessor httpContextAccessor, CryptoUtility cryptoUtility, AppSettings appSettings)
    {
        _httpContextAccessor = httpContextAccessor;
        _cryptoUtility = cryptoUtility;
        _appSettings = appSettings;
    }

    #endregion

    public UserMiniDto? User()
    {
        if (IsAuthenticated() is false)
            return null;

        return new UserMiniDto
        {
            Id = GetUserIdFromToken(),
            FullName = GetClaimValueFromJwtToken(ClaimTypes.Name),
            PersonnelCode = GetClaimValueFromJwtToken(JwtTokenClaimConst.PersonnelCode).ToInt(true),
            UnitName = GetClaimValueFromJwtToken(JwtTokenClaimConst.UnitName),
            PostTitle = GetClaimValueFromJwtToken(JwtTokenClaimConst.PostTitle),
            BoxId = GetClaimValueFromJwtToken(JwtTokenClaimConst.BoxId).ToInt(true),
            Roles = GetRolesFromToken(),
            WorkLocationCode = GetClaimValueFromJwtToken(JwtTokenClaimConst.WorkLocationCode).ToInt(true)
        };
    }

    public bool IsAuthenticated()
    {
        return _httpContextAccessor.HttpContext?.User.Identity?.IsAuthenticated ?? false;
    }

    public bool IsAdmin()
    {
        return GetRolesFromToken().Contains(RoleConst.Identity_Administrator);
    }

    #region Private Methods

    private int GetUserIdFromToken()
    {
        if (IsAuthenticated() is false)
            throw new UnauthorizedAccessException("Current user is not logged in");

        var encryptedUserId = _httpContextAccessor.HttpContext!.User.Claims.First(i => i.Type == ClaimTypes.NameIdentifier).Value;

        if (string.IsNullOrWhiteSpace(encryptedUserId))
            throw new UnauthorizedAccessException("Invalid user");

        var userId = _cryptoUtility.FromEncryptedBase64String(encryptedUserId, _appSettings.AuthSettings!.UserIdEncryptionKey);

        return userId.ToInt();
    }

    private List<string> GetRolesFromToken()
    {
        var stringRoles = GetClaimValueFromJwtToken(ClaimTypes.Role);

        return stringRoles.Split(",").ToList() ?? new List<string>();
    }

    private string GetClaimValueFromJwtToken(string claimType)
    {
        var claimValue = _httpContextAccessor.HttpContext?.User.Claims.FirstOrDefault(i => i.Type == claimType)?.Value;

        return claimValue ?? string.Empty;
    }

    #endregion
}