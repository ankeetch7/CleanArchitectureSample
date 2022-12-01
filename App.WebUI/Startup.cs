using App.Application.Services;
using App.Infrastructure;
using App.Infrastructure.Persistence;
using App.WebUI.Services;
using App.WebUI.StartupServices.AddSwaggerService;
using App.WebUI.StartupServices.Authentication;
using App.WebUI.StartupServicesAndMiddleware.AngularIntegration;
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
            services.AddMvc();
            services.AddControllers();
            services.AddControllersWithViews()
                    .AddJsonOptions(options =>
                        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles
                        ); 
            services.AddHttpContextAccessor();

            services.AddHealthChecks()
                    .AddDbContextCheck<ApplicationDbContext>();

            // add dependency injection
            services.AddInfrastructureServices(_configuration);
            services.AddSingleton<IJwtTokenGenerator,JwtTokenGenerator>();
            services.AddSingleton<ICurrentUserService, CurrentUserService>();

            // swagger services
            services.AddSwaggerConfigureServices();

           // add authentication
           services.AddAuthenticationConfigureService(_configuration);

            //add angular
            services.AddAngularService();
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

            app.UseRouting();

            app.UseSwaggerConfigure();

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
