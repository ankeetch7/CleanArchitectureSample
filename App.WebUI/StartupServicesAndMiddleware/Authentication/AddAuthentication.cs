using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace App.WebUI.StartupServices.Authentication
{
    public static class AddAuthentication
    {
        public static IServiceCollection AddAuthenticationConfigureService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultForbidScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultSignOutScheme = JwtBearerDefaults.AuthenticationScheme;
                options.RequireAuthenticatedSignIn = false;
            })
           .AddJwtBearer(options =>
           {
               options.TokenValidationParameters = new TokenValidationParameters
               {
                   ValidateIssuerSigningKey = true,
                   IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["TokenDefination:JwtKey"])),

                   ValidateIssuer = true,
                   ValidIssuer = configuration["TokenDefination:JwtIssuer"],

                   ValidateAudience = true,
                   ValidAudience = configuration["TokenDefination:JwtAudience"],

                   ValidateLifetime = true,
                   ClockSkew = TimeSpan.Zero,
               };
           });
            return services;
        }
    }
}
