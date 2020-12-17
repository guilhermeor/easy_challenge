using EasyChallenge.Domain;
using EasyChallenge.Domain.Constants;
using FluentAssertions;
using Xunit;

namespace EasyChallenge.Tests.Domain
{
    public class BaseInvestmentTest : BaseDataTest
    {
        [Theory]
        [MemberData(nameof(DataProfitabilityPositive))]
        public void Should_be_process_correctly_IR_when_profitability_is_positive<T>(T entity, decimal irPercent) where T : BaseInvestment
        {
            entity.IR.Should().BeGreaterOrEqualTo(0);
            entity.IR.Should().Be(entity.Profitability() * irPercent);
        }
        [Theory]
        [MemberData(nameof(DataProfitabilityZero))]
        public void Should_be_process_correctly_IR_when_profitability_is_zero<T>(T entity) where T : BaseInvestment => entity.IR.Should().Be(0);

        [Theory]
        [MemberData(nameof(PurchaseAndDueDatesTo6Percent))]
        public void Should_be_loss_6_percent_from_total_value<T>(T entity) where T : BaseInvestment
        {
            entity.RescueValue.Should().BeLessThan(entity.TotalValue);
            entity.RescueValue.Should().BeLessOrEqualTo(entity.TotalValue * RescueTax.THREE_MONTHS);
        }

        [Theory]
        [MemberData(nameof(BaseDataTest.PurchaseAndDueDatesTo15Percent))]
        public void Should_be_loss_15_percent_from_total_value<T>(T entity) where T : BaseInvestment
        {
            entity.RescueValue.Should().BeLessThan(entity.TotalValue);
            entity.RescueValue.Should().BeLessOrEqualTo(entity.TotalValue * RescueTax.HALF);
        }

        [Theory]
        [MemberData(nameof(PurchaseAndDueDatesTo30Percent))]
        public void Should_be_loss_30_percent_from_total_value<T>(T entity) where T : BaseInvestment
        {
            entity.RescueValue.Should().BeLessThan(entity.TotalValue);
            entity.RescueValue.Should().BeLessOrEqualTo(entity.TotalValue * RescueTax.OTHER);
        }
    }
}
