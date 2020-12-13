using Microsoft.Extensions.Caching.Memory;
using System;
using System.Threading.Tasks;

namespace EasyChallenge.Application.Jobs
{
    public struct UpdateCacheJob : IUpdateCacheJob
    {
        private readonly IMemoryCache _cache;
        private readonly IGetSummary _summary;
        public UpdateCacheJob(IMemoryCache cache, IGetSummary summary)
        {
            _cache = cache;
            _summary = summary;
        }

        public void Clear() => _cache.Remove("summary");

        public async Task Update()
        {
            var untilMidnight = DateTime.Today.AddDays(1.0) - DateTime.Now;
            _cache.Set("summary", await _summary.Process(), TimeSpan.FromSeconds(untilMidnight.TotalSeconds));
        }
    }
}
