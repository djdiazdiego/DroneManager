using DroneManager.Core.Abstractions.Persistence;
using DroneManager.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Polly;
using System.Reflection;

namespace DroneManager.Core.Data.Helpers
{
    public static class ApplyChangesHelper
    {
        /// <summary>
        /// Apply pending persistence changes
        /// </summary>
        /// <param name="provider"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task ApplyPendingChanges<TContext>(this IServiceProvider provider, CancellationToken cancellationToken) where TContext : DbContext
        {
            await provider.ApplyPenndingMigrationAsync<TContext>(cancellationToken)
                .ContinueWith(async task => await provider.ApplySeedsAsync(cancellationToken));
        }

        /// <summary>
        /// Execute pending migrations
        /// </summary>
        /// <param name="provider"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        private static async Task ApplyPenndingMigrationAsync<TContext>(this IServiceProvider provider, CancellationToken cancellationToken) where TContext : DbContext
        {
            using var scope = provider.CreateScope();

            var logger = scope.ServiceProvider.GetRequiredService<ILogger<Migration>>();

            if (scope.ServiceProvider.GetRequiredService<TContext>() is TContext context && context.Database.ProviderName != "Microsoft.EntityFrameworkCore.InMemory")
            {
                await ApplyMigrationAsync(context, logger, cancellationToken);
            };

            //if (scope.ServiceProvider.GetRequiredService<IdentityContext>() is IdentityContext identityContext && identityContext.Database.ProviderName != "Microsoft.EntityFrameworkCore.InMemory")
            //{
            //    await ApplyMigrationAsync(identityContext, cancellationToken);
            //}
        }

        /// <summary>
        /// Apply migrations
        /// </summary>
        /// <param name="context"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        private static Task ApplyMigrationAsync(DbContext context, ILogger logger, CancellationToken cancellationToken) =>
            AsyncRetrySyntax.WaitAndRetryAsync(
                Policy.Handle<Exception>(),
                5,
                retryAttempt => TimeSpan.FromSeconds(Math.Pow(2.0, retryAttempt)),
                (ex, time) =>
                {
                    throw ex;
                })
                .ExecuteAsync(async () => await RelationalDatabaseFacadeExtensions.MigrateAsync(context.Database, cancellationToken));

        /// <summary>
        /// Apply seeds
        /// </summary>
        /// <param name="provider"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        private static async Task ApplySeedsAsync(this IServiceProvider provider, CancellationToken cancellationToken)
        {
            //await provider.ApplyIdentitySeedAsync();
            await ApplyApplicationSeedsAsync(provider, cancellationToken);
        }


        /// <summary>
        /// Application seeds
        /// </summary>
        /// <param name="provider"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        private static async Task ApplyApplicationSeedsAsync(this IServiceProvider provider, CancellationToken cancellationToken)
        {
            var seedAssembly = Assembly.Load("DroneManager.Infrastructure");
            var seedTypes = typeof(ISeed).GetConcreteTypes(seedAssembly);

            var tasks = (from seedType in seedTypes
                         let entityType = seedType.BaseType?.GenericTypeArguments
                         let methodInfo = seedType.GetMethod(nameof(ISeed.SeedAsync))
                         let instance = Activator.CreateInstance(seedType)
                         let resultTask = methodInfo.Invoke(instance, new object[]
                         {
                             provider,
                             cancellationToken
                         }) as Task
                         select resultTask).ToArray();

            foreach (var task in tasks)
            {
                await task;
            }
        }

        ///// <summary>
        ///// Identity seeds
        ///// </summary>
        ///// <param name="provider"></param>
        ///// <returns></returns>
        //private static async Task ApplyIdentitySeedAsync(this IServiceProvider provider)
        //{
        //    using var scope = provider.CreateScope();

        //    using var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<Guid>>>();
        //    using var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();

        //    await DefaultRoles.SeedAsync(roleManager)
        //        .ContinueWith(async task => await DefaultSuperAdmin.SeedAsync(userManager, roleManager));
        //}
    }
}
