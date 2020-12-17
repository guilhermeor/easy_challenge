using System;
using System.Collections.Generic;

namespace EasyChallenge.Application.Settings
{
    public record ApiSettings
    {
        public string BaseUri { get; set; }
        public Dictionary<string, string> Uris { get; set; }
    }
}
