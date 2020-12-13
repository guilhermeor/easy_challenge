using System.Collections.Generic;

namespace EasyChallenge.Application
{
    public record ApiSettings
    {
        public Dictionary<string, string> Uris { get; set; }
    }
}
