using EasyChallenge.Application;
using EasyChallenge.Application.Mediators.Investments;
using EasyChallenge.Domain;
using Microsoft.Extensions.Caching.Memory;
using Moq;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;
using OpenTracing;
using EasyChallenge.Application.Services.External;
using EasyChallenge.Application.Settings;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Caching.Distributed;

namespace EasyChallenge.Tests.Mediators
{
    public class InvestmentsHandlerTest : BaseHandlerTest
    {
        private readonly Mock<IPortfolio> _mockPortfolio;
        private readonly Mock<ITracer> _mockTracer;
        private readonly MemoryDistributedCache _cache;
        public InvestmentsHandlerTest()
        {
            _mockPortfolio = new Mock<IPortfolio>();
            _mockTracer = new Mock<ITracer>();
            _mockTracer.Setup(x => x.ActiveSpan.SetTag(It.IsAny<string>(), It.IsAny<string>()));
            var opts = Options.Create(new MemoryDistributedCacheOptions());
            _cache = new MemoryDistributedCache(opts);
        }

        [Fact]
        public async Task Should_be_return_portfolio_when_external_apis_return_200()
        {
            var tdsDto = new TdsDto { Tds = new List<Td>() };
            var lcisDto = new LcisDto { Lcis = new List<Lci>() };
            var fundsDto = new FundsDto { Funds = new List<Fund>() };

            _mockPortfolio.Setup(x => x.GetAsync()).ReturnsAsync(InvestmentsResponse);

            var handler = new InvestimentsHandler(_mockPortfolio.Object, _cache, _mockTracer.Object);

            var response = await handler.Handle(new InvestmentsRequest(), CancellationToken.None);

            response.Result.Should().NotBeNull();
            response.Valid.Should().BeTrue();
            response.Result.Should().BeOfType<InvestmentsResponse>();
        }
        [Fact]
        public async Task Should_be_return_portfolio_using_cache()
        {
            var investmentResponseBytes = System.Text.Json.JsonSerializer.SerializeToUtf8Bytes(InvestmentsResponse);

            _cache.Set(CacheKeys.Portfolio, investmentResponseBytes);            

            var handler = new InvestimentsHandler(_mockPortfolio.Object, _cache, _mockTracer.Object);

            var response = await handler.Handle(new InvestmentsRequest(), CancellationToken.None);

            _mockPortfolio.Verify(x => x.GetAsync(), Times.Never);
            response.Result.Should().NotBeNull();
            response.Valid.Should().BeTrue();
            response.Result.Should().BeOfType<InvestmentsResponse>();

        }
    }
}
