using App.Application.Services;
using App.Infrastructure;
using App.Infrastructure.Persistence;
using App.WebUI.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace App.WebUI
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddControllers();
            services.AddControllersWithViews();
            services.AddHttpContextAccessor();

            services.AddHealthChecks()
                    .AddDbContextCheck<ApplicationDbContext>();
            // add dependency injection
            services.AddInfrastructureServices(_configuration);
            services.AddSingleton<IJwtTokenGenerator,JwtTokenGenerator>();

            //for swagger
            services.AddSwaggerDocument(document =>
            {
                document.Title = "Clean Architecture Web API Project";
                document.Version = "v1";
            });

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultForbidScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["TokenDefination:JwtKey"])),

                    ValidateIssuer = true,
                    ValidIssuer = _configuration["TokenDefination:JwtIssuer"],

                    ValidateAudience = true,
                    ValidAudience = _configuration["TokenDefination:JwtAudience"],

                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero,
                };
            });

        }
        public void Configure(WebApplication app, IWebHostEnvironment env)
        {
            // global exception handling
            app.Use(async (context, next) =>
            {
                try
                {
                    await next();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    context.Response.StatusCode = 500;
                    await context.Response.WriteAsync("Internal Server Error");
                }
            });

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseOpenApi();
            app.UseSwaggerUi3(swagger =>
            {
                swagger.Path = "/api";
            });
            
            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
