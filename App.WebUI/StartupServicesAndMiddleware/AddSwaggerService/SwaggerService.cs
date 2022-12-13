using NSwag;
using NSwag.Generation.Processors.Security;

namespace App.WebUI.StartupServices.AddSwaggerService
{
    public static class SwaggerService
    {
        public static IServiceCollection AddSwaggerConfigureServices(this IServiceCollection services)
        {
            services.AddOpenApiDocument(configure =>
            {
                configure.Title = "Clean Architecture Web API Project";
                configure.AddSecurity("JWT", Enumerable.Empty<string>(), new OpenApiSecurityScheme
                {
                    Type = OpenApiSecuritySchemeType.ApiKey,
                    Name = "Authorization",
                    In = OpenApiSecurityApiKeyLocation.Header,
                    Description = "Type into the textbox: Bearer {your JWT token}."
                });

                configure.OperationProcessors.Add(new AspNetCoreOperationSecurityScopeProcessor("JWT"));
            });

            return services;
        }
        public static WebApplication UseSwaggerConfigure(this WebApplication app)
        {
            app.UseOpenApi();
            app.UseSwaggerUi3(swagger =>
            {
                swagger.Path = "/api";
                swagger.DocumentPath = "/api/specification.json";
            });
            return app;
        }
    }
}
