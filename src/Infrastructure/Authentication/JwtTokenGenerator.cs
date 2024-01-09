using Application.Common.Interfaces.Authentication;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Infrastructure.Authentication
{
    public class JwtTokenGenerator : IJwtTokenGenerator
    {
        private readonly JwtSettings _JwtSettings;

        public JwtTokenGenerator(IOptions<JwtSettings> jwtOptions)
        {
            _JwtSettings = jwtOptions.Value ?? throw new ArgumentNullException(nameof(jwtOptions));
        }

        public string GenerateToken(string userId, string username) {

            var singingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(_JwtSettings.SecretKey)
                ),
                SecurityAlgorithms.HmacSha256
            );

            var claims = new[] {
                new Claim(JwtRegisteredClaimNames.Sub, userId),
                new Claim(JwtRegisteredClaimNames.GivenName, username)
            };

            var securityToken = new JwtSecurityToken(
                issuer: _JwtSettings.Issuer,
                audience: _JwtSettings.Audience,
                expires: DateTime.Now.AddDays(_JwtSettings.ExpiryMinutes),
                claims: claims, 
                signingCredentials: singingCredentials

            );

            return new JwtSecurityTokenHandler().WriteToken(securityToken);
        }
    }
}
