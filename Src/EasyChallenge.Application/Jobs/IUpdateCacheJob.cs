using System.Threading.Tasks;

namespace EasyChallenge.Application.Jobs
{
    public interface IUpdateCacheJob
    {
        Task Update();
        void Clear();
    }
}