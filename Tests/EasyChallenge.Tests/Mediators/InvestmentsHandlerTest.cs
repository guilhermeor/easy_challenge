using EasyChallenge.Application;
using EasyChallenge.Application.Mediators;
using EasyChallenge.Application.Services;
using EasyChallenge.Domain;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;
using OpenTracing;

namespace EasyChallenge.Tests.Mediators
{
    public class InvestmentsHandlerTest : BaseHandlerTest
    {
        private readonly Mock<IGetSummary> _mockSummary;
        private readonly Mock<ITracer> _mockTracer;
        public InvestmentsHandlerTest()
        {
            _mockSummary = new Mock<IGetSummary>();
            _mockTracer = new Mock<ITracer>();
            _mockTracer.Setup(x => x.ActiveSpan.SetTag(It.IsAny<string>(), It.IsAny<string>()));
        }

        [Fact]
        public async Task Should_be_return_summary_when_external_apis_return_200()
        {
            var tdsDto = new TdsDto { Tds = new List<Td>() };
            var lcisDto = new LcisDto { Lcis = new List<Lci>() };
            var fundsDto = new FundsDto { Fundos = new List<Funds>() };
            var cache = new MemoryCache(new MemoryCacheOptions());
            var expected = new InvestmentsResponse();


            _mockSummary.Setup(x => x.Process()).ReturnsAsync(expected);

            var handler = new InvestimentsHandler(_mockSummary.Object, cache, _mockTracer.Object);

            var response = await handler.Handle(new InvestmentsRequest(), CancellationToken.None);

            response.Result.Should().NotBeNull();
            response.Valid.Should().BeTrue();
            response.Result.Should().BeOfType<InvestmentsResponse>();
        }
    }
}
