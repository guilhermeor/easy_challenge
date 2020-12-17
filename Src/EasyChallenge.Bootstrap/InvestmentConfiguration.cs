using EasyChallenge.Application;
using EasyChallenge.Application.MessageHandler;
using EasyChallenge.Application.Services.External;
using EasyChallenge.Application.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Refit;
using System;

namespace EasyChallenge.Bootstrap
{
    public static class InvestmentConfiguration
    {
        public static IServiceCollection ConfigurationPorfotio(this IServiceCollection services, IConfiguration config)
        {
            services.AddTransient<IPortfolio, Portfolio>();
            services.AddTransient<ExternalRequestHandler>();
            services.AddRefitClient<IInvestmentService>().ConfigureHttpClient(c =>
            {
                c.BaseAddress = new Uri(config.GetValue<string>("ApiSettings:BaseUri"));
                c.Timeout = TimeSpan.FromSeconds(config.GetValue<int>("TimeoutHttpRequest"));
            }).AddHttpMessageHandler<ExternalRequestHandler>();

            services.Configure<ApiSettings>(config.GetSection("ApiSettings"));

            return services;
        }
    }
}
