using EasyChallenge.Application.Mediators;
using System.Threading.Tasks;

namespace EasyChallenge.Application
{
    public interface IGetSummary
    {
        Task<InvestmentsResponse> Process();
    }
}
