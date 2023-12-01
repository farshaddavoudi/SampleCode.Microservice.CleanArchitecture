using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using SampleMicroserviceApp.Identity.Domain.ConfigurationSettings;
using SampleMicroserviceApp.Identity.Domain.Utilities;

namespace SampleMicroserviceApp.Identity.Application.Services;

public class AuthTokenUtility
{
    private readonly AppSettings _appSettings;
    private readonly CryptoUtility _cryptoUtility;

    #region ctor

    public AuthTokenUtility(AppSettings appSettings, CryptoUtility cryptoUtility)
    {
        _appSettings = appSettings;
        _cryptoUtility = cryptoUtility;
    }

    #endregion

    public string GenerateJwtToken(List<Claim> userClaims)
    {
        var jwtTokenHandler = new JwtSecurityTokenHandler();

        var jwtSecretKey = Encoding.ASCII.GetBytes(_appSettings.AuthSettings!.JwtSecret);

        var tokenTtl = _appSettings.IsDevelopment
            ? TimeSpan.FromDays(1)
            : _appSettings.AuthSettings.JwtTokenTtl;

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

        var encryptedUserId = _cryptoUtility.ToEncryptedBase64(userId.ToString(), _appSettings.AuthSettings!.UserIdEncryptionKey);

        tokenClaims.Add(new Claim(ClaimTypes.NameIdentifier, encryptedUserId));

        tokenClaims.Add(new Claim(ClaimTypes.Name, name));

        return tokenClaims;
    }

    public string GenerateRefreshToken()
    {
        return Guid.NewGuid().ToString();
    }
}