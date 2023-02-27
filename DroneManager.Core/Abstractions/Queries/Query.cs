using DroneManager.Core.Abstractions.Dtos;
using DroneManager.Core.Wrappers;

namespace DroneManager.Core.Abstractions.Queries
{
    /// <summary>
    /// Base query.
    /// </summary>
    /// <typeparam name="TResponse"></typeparam>
    public abstract class Query<TResponse> : IQuery<Response<TResponse>>
        where TResponse : class
    {
    }
    /// <summary>
    /// Query with entity identifier.
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

    /// <summary>
    /// Base query
    /// </summary>
    /// <typeparam name="Tkey"></typeparam>
    /// <typeparam name="TResponse"></typeparam>
    public abstract class EnumerationQuery<Tkey, TResponse> : IQuery<Response<TResponse>>
             where TResponse : class
    {
        protected EnumerationQuery() { }
    }
}
