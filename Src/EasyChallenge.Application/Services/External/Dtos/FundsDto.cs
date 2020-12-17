using EasyChallenge.Domain;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace EasyChallenge.Application.Services.External
{
    public readonly struct FundsDto
    {
        [JsonProperty("Fundos")]
        public IEnumerable<Fund> Funds { get; init; }
    }
}
