using DroneManager.Core.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace DroneManager.Infrastructure.Contexts
{
    public class DbContextRead : DbContextReadBase
    {
        public DbContextRead([NotNull] DbContextOptions<DbContextRead> options) : base(options)
        {
        }
    }
}
