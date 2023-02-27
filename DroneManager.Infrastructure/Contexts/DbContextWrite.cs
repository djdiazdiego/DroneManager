using DroneManager.Core.Abstractions.Persistence;
using DroneManager.Core.Data.Contexts;
using DroneManager.Core.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace DroneManager.Infrastructure.Contexts
{
    public class DbContextWrite : DbContextWriteBase, IUnitOfWork
    {
        public DbSet<StorageFileData> StorageFileData { get; set; }

        public DbContextWrite([NotNull] DbContextOptions<DbContextWrite> options) : base(options)
        {
        }
    }
}
