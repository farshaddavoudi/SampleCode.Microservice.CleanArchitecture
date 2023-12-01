using System.Security.Claims;

namespace SampleMicroserviceApp.Identity.Application.Common.Extensions;

public static class TokenExtensions
{
    public static List<Claim> ParseTokenClaims(this string accessToken)
    {
        return new();
        //Jose.JWT.Payload<Dictionary<string, object>>(accessToken)
        //    .Select(keyValue => new Claim(keyValue.Key, keyValue.Value.ToString()!))
        //    .ToList();
    }
}