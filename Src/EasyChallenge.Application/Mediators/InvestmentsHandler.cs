using Microsoft.Extensions.Caching.Memory;
using OpenTracing;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace EasyChallenge.Application.Mediators
{
    public class InvestimentsHandler : IBaseHandler<InvestmentsRequest, Response<InvestmentsResponse>>
    {
        private readonly IGetSummary _summary;
        private readonly IMemoryCache _cache;
        private readonly ITracer _tracer;
        public InvestimentsHandler(IGetSummary summary, IMemoryCache cache, ITracer tracer)
        {
            _summary = summary;
            _tracer = tracer;
            _cache = cache;
        }

        public async Task<Response<InvestmentsResponse>> Handle(InvestmentsRequest request, CancellationToken cancellationToken)
        {
            var investmentResponse = await _cache.GetOrCreateAsync("summary",
                async entry =>
                {
                    var untilMidnight = DateTime.Today.AddDays(1.0) - DateTime.Now;
                    entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(untilMidnight.TotalSeconds);
                    _tracer.ActiveSpan.SetTag("cache", false);
                    return await _summary.Process();
                });

            return new Response<InvestmentsResponse>(investmentResponse);
        }
    }
}