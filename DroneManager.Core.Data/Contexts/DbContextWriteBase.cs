using DroneManager.Core.Abstractions.Entities.Interfaces;
using DroneManager.Core.Abstractions.Persistence;
using DroneManager.Core.Data.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace DroneManager.Core.Data.Contexts
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class DbContextWriteBase : DbContext, IUnitOfWork
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        protected DbContextWriteBase(
            [NotNull] DbContextOptions options)
            : base(options)
        {
            ChangeTracker.LazyLoadingEnabled = false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.HasDefaultSchema("DroneManager");
            builder.AddConfiguration(Assembly.Load("DroneManager.Infrastructure"));
            base.OnModelCreating(builder);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries<IEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.SetCreatedDate(DateTimeOffset.UtcNow);
                        break;
                    case EntityState.Modified:
                        entry.Entity.SetLastModified(DateTimeOffset.UtcNow);
                        break;
                }
            }

            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
