using EasyChallenge.Application.Mediators;
using EasyChallenge.Application.Mediators.Investments;
using System.Threading.Tasks;

namespace EasyChallenge.Application
{
    public interface IPortfolio
    {
        Task<InvestmentsResponse> GetAsync();
    }
}
