using CodePulse.API.Repositories.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CodePulse.API.Repositories.Implementation
{
    public class TokenRepository : ITokenRepository
    {
        IConfiguration _configuration;
        public TokenRepository(IConfiguration configuration)
        {
            this._configuration = configuration;   
        }
        public string CreateJwtToken(IdentityUser user, List<string> roles)
        {
            var claims = new List<Claim>()
            {
              new  (ClaimTypes.Name, user.UserName),
              new Claim(ClaimTypes.Role,roles[0])
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration.GetSection("Jwt:Issuer").Get<List<string>>().FirstOrDefault(),
                audience: _configuration["Jwt:Audience"],
                claims:claims,
                expires:DateTime.Now.AddMinutes(15),
                signingCredentials:credentials
                );


            return new JwtSecurityTokenHandler().WriteToken(token);

           
        }
    }
}
