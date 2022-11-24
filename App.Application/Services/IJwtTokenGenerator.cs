using App.Application.Command.ApplicationUser;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Application.Services
{
    public interface IJwtTokenGenerator
    {
        JwtSecurityToken GetToken(AppUser user);
    }
}
