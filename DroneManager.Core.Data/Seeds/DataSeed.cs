using DroneManager.Core.Abstractions.Entities.Interfaces;
using DroneManager.Core.Abstractions.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DroneManager.Core.Data.Seeds
{
    /// <summary>
    /// Seed for entity
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public abstract class DataSeed<TEntity, TContext> : ISeed
        where TEntity : class, IEntity
        where TContext : DbContext
    {
        protected DataSeed() { }

        /// <inheritdoc />
        public abstract Task SeedAsync(IServiceProvider provider, CancellationToken cancelationToken);

        protected async Task SaveChangesAsync(IServiceProvider provider, TEntity[] entities, CancellationToken cancelationToken)
        {
            using var scope = provider.CreateScope();
            using var context = scope.ServiceProvider.GetRequiredService<TContext>();

            var dbSet = context.Set<TEntity>();
            var length = entities.Length;

            for (int i = 0; i < length; i++)
            {
                var exist = await dbSet.AnyAsync(p => p.Id == entities[i].Id, cancelationToken);
                if (!exist)
                    dbSet.Add(entities[i]);
            }

            await context.SaveChangesAsync(cancelationToken);
        }
    }

}
