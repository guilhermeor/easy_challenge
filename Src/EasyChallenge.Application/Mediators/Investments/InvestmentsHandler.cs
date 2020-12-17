using EasyChallenge.Application.Extensions;
using EasyChallenge.Application.Settings;
using Microsoft.Extensions.Caching.Distributed;
using OpenTracing;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Text.Json;

namespace EasyChallenge.Application.Mediators.Investments
{
    public class InvestimentsHandler : IBaseHandler<InvestmentsRequest, Response<InvestmentsResponse>>
    {
        private readonly IPortfolio _portfolio;
        private readonly IDistributedCache _cache;
        private readonly ITracer _tracer;
        public InvestimentsHandler(IPortfolio portfolio, IDistributedCache cache, ITracer tracer)
        {
            _portfolio = portfolio;
            _tracer = tracer;
            _cache = cache;
        }

        public async Task<Response<InvestmentsResponse>> Handle(InvestmentsRequest request, CancellationToken cancellationToken)
        {

            var responseCache = await _cache.GetAsync(CacheKeys.Portfolio, cancellationToken);
            if (responseCache is null)
            {
                _tracer.ActiveSpan.SetTag("cache", false);
                var investmentResponse = await _portfolio.GetAsync();
                _ = _cache.SetAsync(CacheKeys.Portfolio, JsonSerializer.SerializeToUtf8Bytes(investmentResponse), DateTime.Now.UntilMidnight(), cancellationToken);
                return new Response<InvestmentsResponse>(investmentResponse);
            }
            else
            {
                _tracer.ActiveSpan.SetTag("cache", true);
                var investmentResponse = JsonSerializer.Deserialize<InvestmentsResponse>(new ReadOnlySpan<byte>(responseCache));
                return new Response<InvestmentsResponse>(investmentResponse);
            }
        }
    }
}