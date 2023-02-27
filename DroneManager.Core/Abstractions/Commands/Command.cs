using DroneManager.Core.Abstractions.Dtos;
using DroneManager.Core.Wrappers;

namespace DroneManager.Core.Abstractions.Commands
{

    /// <summary>
    /// Command with entity identifier
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public abstract class Command<TKey, TResult> : ICommand<Response<TResult>>
        where TResult : class, IDTO<TKey>
    {
        /// <summary>
        /// Entity identifier
        /// </summary>
        public TKey Id { get; set; }
    }
}
