using App.Application.Command.ApplicationUser;
using App.Application.Services;
using App.Domain.Constants;
using App.Domain.Enums;
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
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, identityUser.Id),
                new Claim(JwtRegisteredClaimNames.UniqueName, identityUser.FullName),
                new Claim(JwtRegisteredClaimNames.Email, identityUser.Email),
                new Claim(Constant.UserName, identityUser.UserName),
                new Claim(Constant.UserType, identityUser.UserType.ToString()),
            };
            if(identityUser.UserType == UserType.User)
            {
                claims.Add(new Claim(Constant.CustomerId, identityUser.CustomerId));
            }

            return claims;
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
