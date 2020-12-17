using EasyChallenge.Domain;
using System;
using System.Text.Json.Serialization;

namespace EasyChallenge.Application.Mediators.Investments
{
    public readonly struct Investment
    {
        public Investment(BaseInvestment investment)
        {
            Name = investment.Name;
            InvestedAmount = investment.InvestedAmount;
            TotalValue = investment.TotalValue;
            DueDate = investment.DueDate;
            IR = investment.IR;
            RescueValue = investment.RescueValue;
        }

        [JsonPropertyName("nome")]
        public string Name { get; init; }
        [JsonPropertyName("valorInvestido")]
        public decimal InvestedAmount { get; init; }
        [JsonPropertyName("valorTotal")]
        public decimal TotalValue { get; init; }
        [JsonPropertyName("vencimento")]
        public DateTime DueDate { get; init; }
        [JsonPropertyName("ir")]
        public decimal IR { get; init; }
        [JsonPropertyName("valorResgate")]
        public decimal RescueValue { get; init; }

        public static implicit operator Investment(BaseInvestment investment) => new (investment);
    }
}
