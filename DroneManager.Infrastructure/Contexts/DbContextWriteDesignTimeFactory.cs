using DroneManager.Core.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.Diagnostics;

namespace DroneManager.Infrastructure.Contexts
{
    public class DbContextWriteDesignTimeFactory : IDesignTimeDbContextFactory<DbContextWrite>
    {
        public DbContextWrite CreateDbContext(string[] args)
        {
            var basePath = Directory.GetCurrentDirectory();
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";
            Trace.WriteLine($"BasePath: {basePath}");
            var configuration = new ConfigurationBuilder()
               .SetBasePath(basePath)
               .AddJsonFile("appsettings.json")
               .AddJsonFile($"appsettings.{environment}.json")
               .Build();

            var sqlServerSettings = configuration.GetSection(nameof(SqlServerSettings)).Get<SqlServerSettings>();
            var connectionString = configuration.GetConnectionString("ProjectCS");

            var builder = new DbContextOptionsBuilder<DbContextWrite>();
            builder.UseSqlServer(connectionString, opts =>
            {
                opts.MigrationsAssembly(sqlServerSettings.MigrationsAssemblyName.Project);
                opts.EnableRetryOnFailure(
                    maxRetryCount: sqlServerSettings.MaxRetryCount,
                    maxRetryDelay: sqlServerSettings.MaxRetryDelay,
                    errorNumbersToAdd: sqlServerSettings.ErrorNumbersToAdd);
            });

            return new DbContextWrite(builder.Options);
        }
    }
}