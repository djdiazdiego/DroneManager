using DroneManager.Core.Wrappers;
using MediatR;

namespace DroneManager.Core.Abstractions.Queries
{
    public interface IQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery, TResponse>
        where TQuery : class, IQuery<TResponse>
        where TResponse : class, IResponse
    {
    }
}
