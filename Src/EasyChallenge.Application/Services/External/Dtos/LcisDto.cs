using EasyChallenge.Domain;
using System.Collections.Generic;
namespace EasyChallenge.Application.Services.External
{
    public readonly struct LcisDto
    {
        public IEnumerable<Lci> Lcis { get; init; }
    }
}
