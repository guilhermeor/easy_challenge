using System.Threading.Tasks;

namespace EasyChallenge.Application.Jobs
{
    public interface IUpdateCacheJob
    {
        Task UpdateAsync();
        Task ClearAsync();
    }
}