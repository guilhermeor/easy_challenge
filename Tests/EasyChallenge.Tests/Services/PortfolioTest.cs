using EasyChallenge.Application;
using EasyChallenge.Application.Mediators.Investments;
using EasyChallenge.Application.Services.External;
using EasyChallenge.Application.Settings;
using EasyChallenge.Domain;
using EasyChallenge.Tests.Domain;
using EasyChallenge.Tests.Mediators;
using FluentAssertions;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace EasyChallenge.Tests.Services
{
    public class PortfolioTest : BaseHandlerTest
    {
        private readonly Mock<IInvestmentService> _mockInvestimentService;
        private readonly Portfolio _portfolio;
        private readonly IOptions<ApiSettings> _options;
        public PortfolioTest()
        {
            _mockInvestimentService = new Mock<IInvestmentService>();
            _options = Options.Create(ApiSettings);
            _portfolio = new Portfolio(_mockInvestimentService.Object, _options);
        }
        [Fact]
        public async Task Should_be_valid_when_external_apis_return_200()
        {
            var expectedTdsDto = new TdsDto() { Tds = new List<Td>()};
            var expectedLcisDto = new LcisDto() { Lcis = new List<Lci>() };
            var expectedFundsDto = new FundsDto() { Funds = new List<Fund>() };

            _mockInvestimentService.Setup(x => x.GetTdsAsync(It.IsAny<string>())).ReturnsAsync(expectedTdsDto);
            _mockInvestimentService.Setup(x => x.GetLcisAsync(It.IsAny<string>())).ReturnsAsync(expectedLcisDto);
            _mockInvestimentService.Setup(x => x.GetFundsAsync(It.IsAny<string>())).ReturnsAsync(expectedFundsDto);

            var result = await _portfolio.GetAsync();
            result.Should().BeOfType<InvestmentsResponse>();
            result.TotalValue.Should().Equals(expectedTdsDto.Tds.Sum(x => x.TotalValue) 
                + expectedLcisDto.Lcis.Sum(x => x.TotalValue) + expectedFundsDto.Funds.Sum(x => x.TotalValue));
            result.Investments.Should().NotBeNull();

        }

    }
}
