using EasyChallenge.Application;
using EasyChallenge.Application.Mediators;
using EasyChallenge.Application.MessageHandler;
using EasyChallenge.Application.Services;
using MediatR;
using MediatR.Pipeline;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Refit;
using System;

namespace EasyChallenge.Bootstrap
{
    public static class SummaryConfiguration
    {
        public static IServiceCollection ConfigurationSummary(this IServiceCollection services, IConfiguration config)
        {
            services.AddTransient<IGetSummary, GetSummary>();
            services.AddTransient<ExternalRequestHandler>();
            services.AddMediatR(typeof(IBaseHandler<,>));
            services.AddScoped(typeof(IRequestExceptionHandler
                    <InvestmentsRequest, Response<InvestmentsResponse>>), typeof(BasePipelineException));
            services.AddRefitClient<IInvestmentService>().ConfigureHttpClient(c =>
            {
                c.BaseAddress = new Uri("http://www.mocky.io/v2");
                c.Timeout = TimeSpan.FromSeconds(10);
            }).AddHttpMessageHandler<ExternalRequestHandler>();

            services.Configure<ApiSettings>(config.GetSection("ApiSettings"));
            return services;
        }
    }
}
