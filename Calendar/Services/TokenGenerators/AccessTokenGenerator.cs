using Calendar.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Calendar.Services.TokenGenerators
{
    public class AccessTokenGenerator
    {
        private readonly AuthenticationConfiguration _configuration;
        //private readonly TokenGenerator _tokenGenerator;
        public AccessTokenGenerator(AuthenticationConfiguration configuration)
        {
            _configuration = configuration;
            //_tokenGenerator = tokenGenerator;
        }

        public string GenerateToken(ApplicationUser User)
        {
            SecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.AccessTokenSecret));
            SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            List<Claim> claims = new List<Claim>()
            {
                new Claim("id", User.Id.ToString()),
                new Claim(ClaimTypes.Email, User.Email),
                new Claim(ClaimTypes.Name, User.UserName),
                new Claim(ClaimTypes.Role, User.Role)
            };
            JwtSecurityToken token = new JwtSecurityToken(_configuration.Issuer, _configuration.Audience, claims, DateTime.UtcNow, DateTime.UtcNow.AddMinutes(_configuration.AccessTokenExpirationMinutes), credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
