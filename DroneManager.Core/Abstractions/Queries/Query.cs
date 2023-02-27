using DroneManager.Core.Abstractions.Dtos;
using DroneManager.Core.Wrappers;

namespace DroneManager.Core.Abstractions.Queries
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TResponse"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public abstract class Query<TKey, TResponse> : IQuery<Response<TResponse>>
        where TResponse : class, IDTO<TKey>
    {
        /// <summary>
        /// Entity identifier
        /// </summary>
        public TKey Id { get; set; }
    }
}
