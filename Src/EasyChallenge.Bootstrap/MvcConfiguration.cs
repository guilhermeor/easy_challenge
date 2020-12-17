using Microsoft.AspNetCore.Builder;

namespace EasyChallenge.Bootstrap
{
    public static class MvcConfiguration
    {
        public static IApplicationBuilder Configure(this IApplicationBuilder app)
        {
            app.UseHttpsRedirection();
            app.UseRouting();
            return app;
        }
    }
}
