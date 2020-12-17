using EasyChallenge.Application;
using EasyChallenge.Application.Mediators.Investments;
using MediatR;
using MediatR.Pipeline;
using Microsoft.Extensions.DependencyInjection;

namespace EasyChallenge.Bootstrap
{
    public static class SummaryConfiguration
    {
        public static IServiceCollection ConfigurationSummary(this IServiceCollection services)
        {

            services.AddMediatR(typeof(IBaseHandler<,>));
            services.AddScoped(typeof(IRequestExceptionHandler
                    <InvestmentsRequest, Response<InvestmentsResponse>>), typeof(InvestmentPipelineException));
            return services;
        }
    }
}
