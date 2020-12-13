using EasyChallenge.Domain;
using Refit;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EasyChallenge.Application.Services
{
    public interface IInvestmentService
    {
        [Get("/{key}")]
        Task<TdsDto> GetTdsAsync(string key);
        [Get("/{key}")]
        Task<LcisDto> GetLcisAsync(string key);
        [Get("/{key}")]
        Task<FundsDto> GetFundsAsync(string key);
    }
}
