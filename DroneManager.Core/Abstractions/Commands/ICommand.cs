using DroneManager.Core.Wrappers;
using MediatR;

namespace DroneManager.Core.Abstractions.Commands
{
    public interface ICommand<out TResponse> : IRequest<TResponse>
        where TResponse : class, IResponse
    {
    }
}
