using EasyChallenge.Domain.Constants;
using Newtonsoft.Json;
using System;

namespace EasyChallenge.Domain
{
    public class Td : BaseInvestment
    {
        [JsonProperty("valorInvestido")]
        public override decimal InvestedAmount { get; init; }
        [JsonProperty("valorTotal")]
        public override decimal TotalValue { get; init; }
        [JsonProperty("vencimento")]
        public override DateTime DueDate { get; init; }
        [JsonProperty("dataDeCompra")]
        public override DateTime PurchaseDate { get; init; }
        public override decimal IR => Profitability() >= 0 ? Profitability() * IRPercents.TDS : 0;
    }

}
