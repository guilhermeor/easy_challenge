using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace EasyChallenge.Application.Mediators.Investments
{
    public readonly struct InvestmentsResponse 
    {
        [JsonPropertyName("valorTotal")]
        public decimal TotalValue => Investments.Sum(i => i.TotalValue);
        [JsonPropertyName("investimentos")]
        public IEnumerable<Investment> Investments { get; init; }

        public InvestmentsResponse(IEnumerable<Investment> investments) => Investments = investments;

    }
}
