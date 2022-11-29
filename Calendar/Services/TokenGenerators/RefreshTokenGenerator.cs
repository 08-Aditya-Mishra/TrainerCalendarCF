using Calendar.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Calendar.Services.TokenGenerators
{
    public class RefreshTokenGenerator
    {
        private readonly AuthenticationConfiguration _configuration;
        public RefreshTokenGenerator(AuthenticationConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateToken()
        {
            SecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.RefreshTokenSecret));
            SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            List<Claim> claims = null;
            JwtSecurityToken token = new JwtSecurityToken(_configuration.Issuer, _configuration.Audience,claims ,DateTime.UtcNow, DateTime.UtcNow.AddMinutes(_configuration.RefreshTokenExpirationMinutes), credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
