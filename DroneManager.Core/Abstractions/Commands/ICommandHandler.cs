using DroneManager.Core.Wrappers;
using MediatR;

namespace DroneManager.Core.Abstractions.Commands
{
    public interface ICommandHandler<in TCommand, TResponse> : IRequestHandler<TCommand, TResponse>
        where TCommand : class, ICommand<TResponse>
         where TResponse : class, IResponse
    {
    }
}
