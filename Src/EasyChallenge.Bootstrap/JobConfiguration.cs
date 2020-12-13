using Microsoft.Extensions.DependencyInjection;
using System;
using Hangfire;
using Hangfire.MemoryStorage;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Builder;
using EasyChallenge.Application.Jobs;

namespace EasyChallenge.Bootstrap
{
    public static class JobConfiguration
    {
        public static IServiceCollection JobConfigurationServices(this IServiceCollection services)
        {
            
            services.AddHangfire((sp, config) =>
            {
                config.UseMemoryStorage();
                config.UseSerializerSettings(new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
            });
            services.AddHangfireServer();

            

            return services;
        }

        public static IApplicationBuilder ConfigurationHangfireDashboard(this IApplicationBuilder app)
        {
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHangfireDashboard(options: new DashboardOptions
                {
                    AppPath = null,
                    Authorization = new[] { new AllowAllConnectionsFilter() },
                    IgnoreAntiforgeryToken = true,
                    StatsPollingInterval = 30000
                });
            });
            RegisterJobs();
            return app;
        }

        private static void RegisterJobs()
        {
            RecurringJob.AddOrUpdate<UpdateCacheJob>("Update Cache", x => x.Update(), Cron.Daily(1), TimeZoneInfo.Local);
            RecurringJob.AddOrUpdate<UpdateCacheJob>("Clear Cache", x => x.Clear(), Cron.Never, TimeZoneInfo.Local);
        }
    }
}

