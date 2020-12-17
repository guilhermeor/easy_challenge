using EasyChallenge.Application.Extensions;
using EasyChallenge.Application.Settings;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace EasyChallenge.Application.Jobs
{
    public struct UpdateCacheJob : IUpdateCacheJob
    {
        private readonly IDistributedCache _cache;
        private readonly IPortfolio _portfolio;
        public UpdateCacheJob(IDistributedCache cache, IPortfolio portfolio)
        {
            _cache = cache;
            _portfolio = portfolio;
        }

        public async Task ClearAsync() => await _cache.RemoveAsync(CacheKeys.Portfolio);

        public async Task UpdateAsync()
        {
            var investmentResponse = await _portfolio.GetAsync();
            _ = _cache.SetAsync(CacheKeys.Portfolio, JsonSerializer.SerializeToUtf8Bytes(investmentResponse), DateTime.Now.UntilMidnight());
        }
    }
}
