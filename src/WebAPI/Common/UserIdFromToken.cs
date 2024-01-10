using System.IdentityModel.Tokens.Jwt;

namespace WebAPI.Common
{
    public class UserIdFromToken
    {
        public static string? GetUserId(string token) {
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadJwtToken(token.Replace("Bearer ", ""));

            var userId = jwtToken.Claims.Where(c => c.Type == JwtRegisteredClaimNames.Sub).FirstOrDefault()?.Value;
            return userId;
        }
    }
}
