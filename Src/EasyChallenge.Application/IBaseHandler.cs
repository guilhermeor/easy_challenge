using MediatR;

namespace EasyChallenge.Application
{
    public interface IBaseHandler<T, K> : IRequestHandler<T, K> where T : IRequest<K>
    {
    }
}
