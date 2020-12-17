using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System.Linq;

namespace EasyChallenge.Bootstrap
{
    public static class SwaggerConfiguration
    {
        public static IServiceCollection ConfigurationSwaggerServices(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "EasyChallenge.API", Version = "v1" });
            });
            return services;
        }
        public static IApplicationBuilder ConfigurationSwagger(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.DisplayRequestDuration();
                c.SwaggerEndpoint($"/swagger/v1/swagger.json", "EasyChallenge.API v1");
                c.RoutePrefix = "docs";
            });
            return app;
        }
    }
}
