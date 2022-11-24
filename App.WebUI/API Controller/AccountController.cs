using App.Application.Command.ApplicationUser;
using App.Application.Services;
using App.Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using YamlDotNet.Core.Tokens;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace App.WebUI.API_Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        public AccountController(UserManager<ApplicationUser> userManager,
                                 SignInManager<ApplicationUser> signInManager,
                                 IJwtTokenGenerator jwtTokenGenerator)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtTokenGenerator = jwtTokenGenerator;
        }
        [HttpPost("create-user")]
        [AllowAnonymous]
        public async Task<ActionResult<string>> CreateUser([FromBody] CreateApplicationUserCommand command)
        {
            var user = new ApplicationUser
            {
                Id = Guid.NewGuid().ToString(),
                FullName = command.FullName,
                UserName = command.UserName,
                Email = command.Email,
                PhoneNumber = command.PhoneNumber,
                UserType = command.UserType,
                IsActive = command.IsActive,
                Image = command.Image
            };

            IdentityResult result = await _userManager.CreateAsync(user,command.Password);

            if (!result.Succeeded)
                return BadRequest();

            return Ok(user);
        }

        [HttpPost("authenticate-user")]
        public async Task<IActionResult> AuthenticateUser([FromBody] AuthenticateRequest request)
        {
            var identityUser = await _userManager.FindByNameAsync(request.UserName);

            if (identityUser is null || identityUser.IsActive is false)
                return Unauthorized();

            var result = await _signInManager.CheckPasswordSignInAsync(identityUser, request.Password,lockoutOnFailure: false);

            if (!result.Succeeded)
                return Unauthorized();

            var user = new AppUser
            {
                Id = identityUser.Id,
                FullName = identityUser.FullName,
                UserName = identityUser.UserName,
                Email = identityUser.Email,
                PhoneNumber = identityUser.PhoneNumber,
            };

            var securitToken = _jwtTokenGenerator.GetToken(user);

            var tokenResult = new
            {
                token = new JwtSecurityTokenHandler().WriteToken(securitToken),
                expiration = securitToken.ValidTo
            };

            return Ok(tokenResult);
        }
    }
}
