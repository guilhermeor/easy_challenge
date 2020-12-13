using EasyChallenge.Domain;
using System.Collections.Generic;
using System.Linq;

namespace EasyChallenge.Application.Mediators
{
    public readonly struct InvestmentsResponse 
    {
        public decimal TotalValue => Investments.Sum(i => i.TotalValue);
        public IEnumerable<BaseInvestment> Investments { get; }

        public InvestmentsResponse(IEnumerable<BaseInvestment> investments) => Investments = investments;

    }
}
