using DroneManager.Core.Data.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace DroneManager.Core.Data.Contexts
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class DbContextReadBase : DbContext
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        protected DbContextReadBase(
            [NotNull] DbContextOptions options)
            : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
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
    }
}
