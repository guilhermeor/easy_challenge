﻿using EasyChallenge.Domain.Constants;
using Newtonsoft.Json;
using System;

namespace EasyChallenge.Domain
{

    public class Lci : BaseInvestment
    {
        [JsonProperty("capitalInvestido")]
        public override decimal InvestedAmount { get; init; }
        [JsonProperty("capitalAtual")]
        public override decimal TotalValue { get; init; }
        [JsonProperty("vencimento")]
        public override DateTime DueDate { get; init; }
        [JsonProperty("dataOperacao")]
        public override DateTime PurchaseDate { get; init; }
        public override decimal IR => Profitability() >= 0 ? Profitability() * IRPercents.LCIS : 0;
    }

}
