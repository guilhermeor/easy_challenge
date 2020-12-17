using EasyChallenge.Domain.Constants;
using Newtonsoft.Json;
using System;

namespace EasyChallenge.Domain
{

    public class Fund : BaseInvestment
    {

        [JsonProperty("capitalInvestido")]
        public override decimal InvestedAmount { get; init; }
        [JsonProperty("ValorAtual")]
        public override decimal TotalValue { get; init; }
        [JsonProperty("dataResgate")]
        public override DateTime DueDate { get; init; }
        [JsonProperty("dataCompra")]
        public override DateTime PurchaseDate { get; init; }
        public override decimal IR => Profitability() >= 0 ? Profitability() * IRPercents.FUNDS : 0;
    }

}
