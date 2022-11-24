using App.Application.Command.ApplicationUser;
using App.Application.Services;
using App.Infrastructure.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace App.WebUI.Services
{
    public class JwtTokenGenerator : IJwtTokenGenerator
    {
        private readonly IConfiguration _configuration;
        public JwtTokenGenerator(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public JwtSecurityToken GetToken(AppUser user)
        {
            return GenerateToken(GetClaims(user));
        }

        public List<Claim> GetClaims(AppUser identityUser)
        {
            return new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, identityUser.Id),
                new Claim(JwtRegisteredClaimNames.UniqueName, identityUser.FullName),
                new Claim(JwtRegisteredClaimNames.Email, identityUser.Email)
            };
        }

        public JwtSecurityToken GenerateToken(List<Claim> userClaims)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["TokenDefination:JwtKey"]));

            return new JwtSecurityToken(issuer: _configuration["TokenDefination:JwtIssuer"],
                                                           audience: _configuration["TokenDefination:JwtAudience"],
                                                           claims: userClaims,
                                                           expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(_configuration["TokenDefination:JwtValidMinutes"])),
                                                           signingCredentials: new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256));
        }
    }
}
