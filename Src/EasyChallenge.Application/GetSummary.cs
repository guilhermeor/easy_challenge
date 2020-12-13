using EasyChallenge.Application.Mediators;
using EasyChallenge.Application.Services;
using EasyChallenge.Domain;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyChallenge.Application
{
    public class GetSummary : IGetSummary
    {
        private readonly IInvestmentService _investmentService;
        private readonly Dictionary<string, string> _keys;
        public GetSummary(IInvestmentService investmentService, IOptions<ApiSettings> options)
        {
            _investmentService = investmentService;
            _keys = options.Value.Uris;
        }

        public async Task<InvestmentsResponse> Process()
        {
            var tds = _investmentService.GetTdsAsync(_keys["Tds"]);
            var lcis = _investmentService.GetLcisAsync(_keys["Lcis"]);
            var funds = _investmentService.GetFundsAsync(_keys["Funds"]);

            var result = (await tds).Tds.Cast<BaseInvestment>()
                .Concat((await lcis).Lcis.Cast<BaseInvestment>())
                .Concat((await funds).Fundos.Cast<BaseInvestment>());
            return new InvestmentsResponse(result);
        }
    }
}
