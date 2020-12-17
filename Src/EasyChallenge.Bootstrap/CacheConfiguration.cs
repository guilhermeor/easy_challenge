using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EasyChallenge.Bootstrap
{
    public static class CacheConfiguration
    {
        public static IServiceCollection CacheConfigurationServices(this IServiceCollection services, IConfiguration config)
        {
            var redisConnecttion = config.GetConnectionString("Redis");
            if (!string.IsNullOrEmpty(redisConnecttion))
            {
                services.AddDistributedRedisCache(options =>
                {
                    options.Configuration = redisConnecttion;
                    options.InstanceName = "Portfolio:";
                });
            }
            else
                services.AddDistributedMemoryCache();
            return services;

        }
    }
}
