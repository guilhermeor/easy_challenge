using EasyChallenge.Application.Mediators.Investments;
using EasyChallenge.Application.Settings;
using System;
using System.Collections.Generic;

namespace EasyChallenge.Tests.Mediators
{
    public class BaseHandlerTest
    {
        private readonly static List<Investment> _investments = new()
            {
                new Investment{
                    Name = "Test Investment",
                    InvestedAmount = 12.57M,
                    TotalValue = 16.85M,
                    DueDate = DateTime.Now.AddMonths(12),
                    IR = 0.65M,
                    RescueValue = 14.35M
                }
            };
        protected static ApiSettings ApiSettings =>
            new()
            {
                BaseUri = "http://www.mocky.io/v2",
                Uris = new Dictionary<string, string>()
                {
                    {"Tds","5e3428203000006b00d9632a" },
                    {"Lcis","5e3428203000006b00d9632a" },
                    {"Funds","5e342ab33000008c00d96342" }
                }
            };
        protected static InvestmentsResponse InvestmentsResponse => new(_investments);
    }
}
