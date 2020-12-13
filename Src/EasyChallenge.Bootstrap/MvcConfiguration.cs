using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;

namespace EasyChallenge.Bootstrap
{
    public static class MvcConfiguration
    {
        public static IApplicationBuilder Configure(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseHttpsRedirection();
            app.UseRouting();
            return app;
        }
    }
}
