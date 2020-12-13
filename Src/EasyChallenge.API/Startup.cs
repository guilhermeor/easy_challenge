using EasyChallenge.API.Presenter;
using EasyChallenge.Bootstrap;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace EasyChallenge.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddMemoryCache();
            services.AddTransient<IBasePresenter, BasePresenter>();

            services.ConfigurationSummary(Configuration);

            services.ConfigurationSwaggerServices();
            services.ConfigurationJaeger();

            services.JobConfigurationServices();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.Configure(env);
            app.ConfigurationSwagger();
            app.ConfigurationHangfireDashboard();
        }
    }
}
