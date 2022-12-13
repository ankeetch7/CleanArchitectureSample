namespace App.WebUI.StartupServicesAndMiddleware.AngularIntegration
{
    public static class AngularIntegrationServiceAndMiddleware
    {
        public static IServiceCollection AddAngularService(this IServiceCollection services)
        {
            services.AddSpaStaticFiles(config =>
            {
                config.RootPath = "ClientApp/dist";
            });
            return services;
        }
        public static WebApplication UseAngular(this WebApplication app, IWebHostEnvironment env)
        {
            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";
                if (env.IsDevelopment())
                {
                    spa.UseProxyToSpaDevelopmentServer("http://localhost:4200/");
                }
            });
            return app;
        }
    }
}
