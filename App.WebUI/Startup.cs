using App.Application;
using App.Application.Command.Product.CreateProduct;
using App.Application.Services;
using App.Infrastructure;
using App.Infrastructure.Persistence;
using App.WebUI.Services;
using App.WebUI.StartupServices.AddSwaggerService;
using App.WebUI.StartupServices.Authentication;
using App.WebUI.StartupServicesAndMiddleware.AngularIntegration;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Serialization;
using System.Text.Json.Serialization;

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
            services.AddMvc()
                .AddFluentValidation(opt =>
                    opt.RegisterValidatorsFromAssemblyContaining<CreateProductCommandValidator>()
                );
                

            services.AddControllersWithViews(options =>
            {
                var defaultPolicy = new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme)
                       .RequireAuthenticatedUser()
                       .Build();

                options.Filters.Add(new AuthorizeFilter(defaultPolicy));
            })
            .AddJsonOptions(options =>
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles
            ); 
            services.AddHttpContextAccessor();

            services.AddHealthChecks()
                    .AddDbContextCheck<ApplicationDbContext>();

            services.AddCors();

            // add dependency injection
            services.AddInfrastructureServices(_configuration);
            services.AddApplicationDependencyInjection();
            services.AddSingleton<IJwtTokenGenerator,JwtTokenGenerator>();
            services.AddSingleton<ICurrentUserService, CurrentUserService>();

            // swagger services
            services.AddSwaggerConfigureServices();

           // add authentication
           services.AddAuthenticationConfigureService(_configuration);

            //add angular
            services.AddAngularService();

            services.AddAuthorization();

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

            app.UseCors(cors =>
            {
                cors.AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowAnyOrigin();
            });

            app.UseStaticFiles();

            // available in SwaggerService class
            app.UseSwaggerConfigure();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            //app.MapControllerRoute(
            //    name: "default",
            //    pattern: "{controller=Home}/{action=Index}/{id?}");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(name: "Default",
                                             pattern: "{controller}/{action=Index}/{id?}");
            });

            // angular app
            app.UseAngular(env);

            app.Run();
        }
    }
}
