using EasyChallenge.Domain;
using System.Collections.Generic;

namespace EasyChallenge.Application.Services.External
{
    public readonly struct TdsDto
    {
        public IEnumerable<Td> Tds { get; init; }
    }
}
