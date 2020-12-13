using EasyChallenge.Domain.Constants;
using Newtonsoft.Json;
using System;

namespace EasyChallenge.Domain
{
    public abstract class BaseInvestment
    {
        public abstract decimal InvestedAmount { get; init; }
        public abstract decimal TotalValue { get; init; }
        public abstract DateTime DueDate { get; init; }
        public abstract DateTime PurchaseDate { get; init; }
        [JsonProperty("iof")]
        public float Iof { get; init; }
        [JsonProperty("nome")]
        public string Name { get; init; }
        public abstract decimal IR { get; }
        public decimal RescueValue
        {
            get
            {
                var totalSeconds = (DateTime.Now - PurchaseDate).TotalSeconds;
                var percent = totalSeconds / (DueDate - PurchaseDate).TotalSeconds;

                if ((DueDate - DateTime.Now).Days <= 90)
                    return TotalValue * RescueTax.THREE_MONTHS;

                if (percent > 0.5)
                    return TotalValue * RescueTax.HALF;

                return TotalValue * RescueTax.OTHER;
            }
        }
        public decimal Profitability() => TotalValue - InvestedAmount;

    }
}
