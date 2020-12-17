using EasyChallenge.Application;
using EasyChallenge.Application.Jobs;
using EasyChallenge.Application.Mediators.Investments;
using EasyChallenge.Application.Settings;
using EasyChallenge.Tests.Mediators;
using FluentAssertions;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace EasyChallenge.Tests.Jobs
{
    public class CacheJobsTest : BaseHandlerTest
    {
        private readonly Mock<IPortfolio> _mockPortfolio;
        private UpdateCacheJob _updateCacheJob;
        private readonly MemoryDistributedCache _cache;
        public CacheJobsTest()
        {
            _mockPortfolio = new Mock<IPortfolio>();
            var opts = Options.Create(new MemoryDistributedCacheOptions());
            _cache = new MemoryDistributedCache(opts);
        }
        [Fact]
        public async Task Should_be_valid_when_job_is_requested_to_clear_cache()
        {
            await _cache.SetAsync(CacheKeys.Portfolio, Array.Empty<byte>());

            _updateCacheJob = new UpdateCacheJob(_cache, _mockPortfolio.Object);
            await _updateCacheJob.ClearAsync();
            _cache.Get(CacheKeys.Portfolio).Should().BeNull();
        }

        [Fact]
        public async Task Should_be_valid_when_job_is_requested_to_clear_cache_and_cache_is_empty()
        {
            _updateCacheJob = new UpdateCacheJob(_cache, _mockPortfolio.Object);
            await _updateCacheJob.ClearAsync();
            _cache.Get(CacheKeys.Portfolio).Should().BeNull();
        }

        [Fact]
        public async Task Should_be_valid_when_job_is_requested_to_update_cache_and_cache_is_empty()
        {
            _updateCacheJob = new UpdateCacheJob(_cache, _mockPortfolio.Object);

            _mockPortfolio.Setup(x => x.GetAsync()).ReturnsAsync(InvestmentsResponse);
            await _updateCacheJob.UpdateAsync();
            _cache.Get(CacheKeys.Portfolio).Should().NotBeNull();
        }

        [Fact]
        public async Task Should_be_valid_when_job_is_requested_to_update_cache_and_cache_is_not_empty()
        {
            _cache.Set(CacheKeys.Portfolio, Array.Empty<byte>());
            _updateCacheJob = new UpdateCacheJob(_cache, _mockPortfolio.Object);
            _mockPortfolio.Setup(x => x.GetAsync()).ReturnsAsync(InvestmentsResponse);
            await _updateCacheJob.UpdateAsync();
            _cache.Get(CacheKeys.Portfolio).Should().NotBeNull();
        }
    }
}
