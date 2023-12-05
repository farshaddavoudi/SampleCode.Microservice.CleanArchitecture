using Microsoft.IdentityModel.Tokens;
using SampleMicroserviceApp.Identity.Domain.ConfigurationSettings;
using SampleMicroserviceApp.Identity.Domain.Utilities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SampleMicroserviceApp.Identity.Application.Services;

public class AuthTokenUtility(AppSettings appSettings, CryptoUtility cryptoUtility)
{
    public string GenerateJwtToken(List<Claim> userClaims)
    {
        var jwtTokenHandler = new JwtSecurityTokenHandler();

        var jwtSecretKey = Encoding.ASCII.GetBytes(appSettings.AuthSettings!.JwtSecret);

        var tokenTtl = appSettings.IsDevelopment
            ? TimeSpan.FromDays(1)
            : appSettings.AuthSettings.JwtTokenTtl;

        // Token descriptor
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(userClaims),
            Expires = DateTime.Now.Add(tokenTtl),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(jwtSecretKey), SecurityAlgorithms.HmacSha256)
        };

        var token = jwtTokenHandler.CreateToken(tokenDescriptor);

        return jwtTokenHandler.WriteToken(token);
    }

    public List<Claim> GetJwtTokenClaims(int userId, string name)
    {
        var tokenClaims = new List<Claim>();

        var encryptedUserId = cryptoUtility.ToEncryptedBase64(userId.ToString(), appSettings.AuthSettings!.UserIdEncryptionKey);

        tokenClaims.Add(new Claim(ClaimTypes.NameIdentifier, encryptedUserId));

        tokenClaims.Add(new Claim(ClaimTypes.Name, name));

        return tokenClaims;
    }

    public string GenerateRefreshToken()
    {
        return Guid.NewGuid().ToString();
    }
}