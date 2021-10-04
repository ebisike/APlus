using APlus.Infacstructure.Jwt.Settings;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace APlus.Infacstructure.Jwt
{
    public class JwtOAuth
    {
        private readonly JwtSettings _jwtSettings;

        public JwtOAuth(IOptions<JwtSettings> jwtSeetings)
        {
            _jwtSettings = jwtSeetings.Value;
        }
        public string GenerateToken(string user_id, string username)
        {
            var expirationDay = DateTime.UtcNow.AddDays(_jwtSettings.ExpirationTime); //Convert.ToDouble(_jwtsettings.ExpirationTime);
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtSettings.Secret));
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, username),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(ClaimTypes.NameIdentifier, user_id),
                    new Claim(ClaimTypes.Name, username),
                }),
                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature),
                Issuer = _jwtSettings.Site,
                Audience = _jwtSettings.Audience,
                Expires = DateTime.UtcNow.AddDays(_jwtSettings.ExpirationTime)
            };

            SecurityToken Token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(Token);
        }
    }
}
