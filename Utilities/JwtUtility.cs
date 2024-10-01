using Microsoft.IdentityModel.Tokens;
using Software2_API.Dtos;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Software2_API.Utilities
{
    /// <summary>JwtHelper</summary>
    public static class JwtUtility
    {
        /// <summary></summary>
        public static ResponseInicionSesionDto? GenTokenkey(ResponseInicionSesionDto userToken, JwtSettingsDto jwtSettings)
        {
            try
            {
                if (userToken == null) throw new ArgumentException(nameof(userToken));
                // Get secret key
                var key = System.Text.Encoding.ASCII.GetBytes(jwtSettings.IssuerSigningKey);
                DateTime expireTime = DateTime.Now;
                if (jwtSettings.FlagExpirationTimeHours)
                {
                    expireTime = DateTime.Now.AddHours(jwtSettings.ExpirationTimeHours);
                }
                else
                {
                    if (jwtSettings.FlagExpirationTimeMinutes)
                    {
                        expireTime = DateTime.Now.AddMinutes(jwtSettings.ExpirationTimeMinutes);
                    }
                    else
                    {
                        return null;
                    }
                }
                IEnumerable<Claim> claims = new Claim[] {
                    new Claim("TiempoExpiracion", expireTime.ToString("yyyy-MM-dd HH:mm:ss")),
                    new Claim("Usuario", "OscarGomez")
                };
                var JWToken = new JwtSecurityToken(
                    issuer: jwtSettings.ValidIssuer, 
                    audience: jwtSettings.ValidAudience, 
                    claims: claims, 
                    notBefore: new DateTimeOffset(DateTime.Now).DateTime, 
                    expires: new DateTimeOffset(expireTime).DateTime, 
                    signingCredentials: new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256));
                userToken.Token = new JwtSecurityTokenHandler().WriteToken(JWToken);
                userToken.TiempoExpiracion = expireTime;
                return userToken;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
