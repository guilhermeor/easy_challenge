using EasyChallenge.Application.Mediators.Investments;
using EasyChallenge.Domain;
using EasyChallenge.Tests.Domain;
using FluentAssertions;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace EasyChallenge.Tests.Mediators
{
    public class InvestmentResponseTest : BaseDataTest
    {
        [Fact]
        public void Should_be_a_valid_investment_response()
        {
            var investments = new List<Investment>()
            {
                new Investment{TotalValue = 89},
                new Investment{TotalValue = 16}
            };

            var result = new InvestmentsResponse(investments);
            result.Investments.Should().Equal(investments);
            result.TotalValue.Should().Equals(investments.Sum(x => x.TotalValue));
        }
        [Theory]
        [MemberData(nameof(DataInvestment))]
        public void Should_be_a_valid_investment<T>(T entity) where T : BaseInvestment
        {
            var result = new Investment(entity);
            result.Name.Should().Equals(entity.Name);
            result.InvestedAmount.Should().Equals(entity.InvestedAmount);
            result.TotalValue.Should().Equals(entity.TotalValue);
            result.DueDate.Should().Equals(entity.DueDate);
        }
    }
}
