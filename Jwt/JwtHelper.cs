using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GoSakaryaApp.WebApi.Jwt
{
    // Jwt oluşturmaya yarar. AuthController -> Login
    public static class JwtHelper
    {
        public static string GenerateJwtToken(JwtDto jwtInfo)
        {
            // Gizli anahtar byte dizisine dönüştürülür. Bu anahtar, token'ı imzalamak için kullanılır.
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtInfo.SecretKey));

            // İmzalama kimlik bilgileri oluşturulur. Gizli anahtar ve imzalama algoritması belirtilir.
            var credentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            // Token'a eklenecek claim'ler oluşturulur. Bu claim'ler, kullanıcı hakkında bilgi içerir.
            var claims = new[]
            {
                new Claim(JwtClaimNames.Id, jwtInfo.Id.ToString()),
                new Claim(JwtClaimNames.Email, jwtInfo.Email),
                new Claim(JwtClaimNames.FirstName, jwtInfo.FirstName),
                new Claim(JwtClaimNames.LastName, jwtInfo.LastName),
                new Claim(JwtClaimNames.UserType, jwtInfo.UserType.ToString()),

                new Claim(ClaimTypes.Role, jwtInfo.UserType.ToString())
            };

            // Token'ın geçerlilik süresi hesaplanır.
            var expireTime = DateTime.UtcNow.AddMinutes(jwtInfo.ExpireMinutes);

            // JWT oluşturulur.  Issuer (yayımcı), Audience (kullanıcı), claim'ler, geçerlilik süresi ve imzalama kimlik bilgileri belirtilir.
            var tokenDescriptor = new JwtSecurityToken(jwtInfo.Issuer, jwtInfo.Audience, claims, null, expireTime, credentials);

            // JWT, string formatına dönüştürülür.
            var token = new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
            
            return token;
        }
    }
}
