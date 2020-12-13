using EasyChallenge.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyChallenge.Tests.Mediators
{
    public class BaseHandlerTest
    {
        protected static ApiSettings ApiSettings =>
            new()
            {
                Uris = new Dictionary<string, string>()
                {
                    {"Tds","5e3428203000006b00d9632a" },
                    {"Lcis","5e3428203000006b00d9632a" },
                    {"Funds","5e342ab33000008c00d96342" }
                }
            };
    }
}
