using DroneManager.Core.Abstractions.Entities.Interfaces;
using DroneManager.Core.Abstractions.Persistence;
using Microsoft.EntityFrameworkCore;

namespace DroneManager.Core.Data.Repositories
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class QueryRepository<TEntity> : IQueryRepository<TEntity> where TEntity : class, IEntity
    {
        private readonly DbContext _context;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbContext"></param>
        public QueryRepository(DbContext dbContext) => _context = dbContext;

        /// <inheritdoc />
        public IQueryable<TEntity> GetQuery() => _context.Set<TEntity>();

        /// <inheritdoc />
        public async ValueTask<TEntity?> FindAsync(object[] keyValues, CancellationToken cancelationToken) =>
            await _context.Set<TEntity>().FindAsync(keyValues, cancelationToken);
    }
}
