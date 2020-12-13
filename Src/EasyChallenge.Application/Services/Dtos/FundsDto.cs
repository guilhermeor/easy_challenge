using EasyChallenge.Domain;
using System.Collections.Generic;

namespace EasyChallenge.Application.Services
{
    public readonly struct FundsDto
    {
        public IEnumerable<Funds> Fundos { get; init; }
    }
}
