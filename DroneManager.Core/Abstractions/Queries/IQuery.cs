using DroneManager.Core.Wrappers;
using MediatR;

namespace DroneManager.Core.Abstractions.Queries
{
    public interface IQuery<out TResponse> : IRequest<TResponse>
        where TResponse : class, IResponse
    {
    }
}
