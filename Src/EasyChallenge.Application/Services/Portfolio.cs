using EasyChallenge.Application.Mediators.Investments;
using EasyChallenge.Application.Services.External;
using EasyChallenge.Application.Settings;
using EasyChallenge.Domain;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyChallenge.Application
{
    public class Portfolio : IPortfolio
    {
        private readonly IInvestmentService _investmentService;
        private readonly Dictionary<string, string> _keys;
        public Portfolio(IInvestmentService investmentService, IOptions<ApiSettings> options)
        {
            _investmentService = investmentService;
            _keys = options.Value.Uris;
        }

        public async Task<InvestmentsResponse> GetAsync()
        {
            var tdsDto = _investmentService.GetTdsAsync(_keys["Tds"]);
            var lcisDto = _investmentService.GetLcisAsync(_keys["Lcis"]);
            var fundsDto = _investmentService.GetFundsAsync(_keys["Funds"]);

            var result = (await tdsDto).Tds.Cast<BaseInvestment>()
                .Concat((await lcisDto).Lcis.Cast<BaseInvestment>())
                .Concat((await fundsDto).Funds.Cast<BaseInvestment>());

            return new InvestmentsResponse(result.Select(x => (Investment)x));
        }
    }
}
