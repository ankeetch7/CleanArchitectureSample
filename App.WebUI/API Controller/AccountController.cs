using App.Application.Command.ApplicationUser;
using App.Application.Interfaces;
using App.Application.Services;
using App.Domain.Entities;
using App.Domain.Enums;
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
        private readonly IUnitOfWork _unitOfWork;
        public AccountController(UserManager<ApplicationUser> userManager,
                                 SignInManager<ApplicationUser> signInManager,
                                 IJwtTokenGenerator jwtTokenGenerator,
                                 IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtTokenGenerator = jwtTokenGenerator;
            _unitOfWork = unitOfWork;
        }

        [HttpPost("create-user")]
        [AllowAnonymous]
        public async Task<ActionResult<string>> CreateUser([FromBody] CreateApplicationUserCommand command)
        {
            var customer = new Customer
            {
                Id = Guid.NewGuid(),
                FullName = command.FullName,
                Email = command.Email,
                Phone = command.PhoneNumber,
                Gender = command.Gender,
                DateOfBirth = command.DateOfBirth,
                Address = command.Address
            };

            if (command.UserType is UserType.User)
            {
                _unitOfWork.Customers.Add(customer);
                await _unitOfWork.SaveChangesAsync();
            }

            var user = new ApplicationUser
            {
                Id = Guid.NewGuid().ToString(),
                FullName = command.FullName,
                UserName = command.UserName,
                Email = command.Email,
                PhoneNumber = command.PhoneNumber,
                UserType = command.UserType,
                IsActive = command.IsActive,
                Gender = command.Gender,
                DateOfBirth = command.DateOfBirth,
                Image = command.Image,
                Address = command.Address,
                CustomerId = command.UserType is UserType.User ? customer.Id : null,
            };

            IdentityResult result = await _userManager.CreateAsync(user,command.Password);

            if (!result.Succeeded)
                return BadRequest();

            return Ok(user.Id);
        }

        [HttpPost("authenticate-user")]
        [AllowAnonymous]
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
                UserType = identityUser.UserType,
                CustomerId = identityUser.CustomerId.ToString()
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
