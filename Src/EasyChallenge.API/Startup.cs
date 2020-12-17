using EasyChallenge.API.Presenter;
using EasyChallenge.Bootstrap;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.IO.Compression;

namespace EasyChallenge.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration) => Configuration = configuration;

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddTransient<IBasePresenter, BasePresenter>();
            services.AddResponseCompression();
            services.Configure<BrotliCompressionProviderOptions>(options => options.Level = CompressionLevel.Fastest);

            services.CacheConfigurationServices(Configuration);            
            services.ConfigurationPorfotio(Configuration);
            services.ConfigurationSummary();
            services.ConfigurationSwaggerServices();
            services.ConfigurationJaeger();
            services.JobConfigurationServices();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.Configure();
            app.ConfigurationSwagger();
            app.ConfigurationHangfireDashboard();
        }
    }
}
