using EasyChallenge.Bootstrap.Filters;
using Jaeger;
using Jaeger.Reporters;
using Jaeger.Samplers;
using Jaeger.Senders.Thrift;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OpenTracing;
using OpenTracing.Util;
using System.Reflection;

namespace EasyChallenge.Bootstrap
{
    public static class TracingConfiguration
    {
        public static IServiceCollection ConfigurationJaeger(this IServiceCollection services)
        {
            
            services.AddMvc(options =>
            {
                options.Filters.AddService<TracingActionFilter>();
            });
            services.AddScoped<TracingActionFilter>();
            services.AddSingleton(serviceProvider =>
            {
                string serviceName = Assembly.GetEntryAssembly().GetName().Name;

                ILoggerFactory loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();

                ISampler sampler = new ConstSampler(sample: true);
                var reporter = new RemoteReporter.Builder().WithLoggerFactory(loggerFactory).WithSender(new UdpSender("host.docker.internal", 6831, 0)).Build();
                ITracer tracer = new Tracer.Builder(serviceName)
                    .WithLoggerFactory(loggerFactory)
                    .WithReporter(reporter)
                    .WithSampler(sampler)
                    .Build();

                GlobalTracer.Register(tracer);

                return tracer;
            });

            services.AddOpenTracing();
            return services;
        }
    }
}
