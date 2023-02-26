using DroneManager.Core.Abstractions.Entities.Interfaces;
using DroneManager.Core.Abstractions.Persistence;
using DroneManager.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DroneManager.Core.Data.Seeds
{
    /// <summary>
    /// Seed for enuneration entity
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public abstract class EnumerationSeed<TEntity, TContext> : ISeed
        where TEntity : class, IEnumeration
        where TContext : DbContext
    {
        protected EnumerationSeed() { }

        /// <inheritdoc />
        public virtual async Task SeedAsync(IServiceProvider provider, CancellationToken cancelationToken)
        {
            using var scope = provider.CreateScope();

            var enumType = typeof(TEntity).BaseType?.GetGenericArguments().First();
            if (enumType != null)
            {
                var enumValues = Enum.GetValues(enumType);

                using var context = scope.ServiceProvider.GetRequiredService<TContext>();

                foreach (Enum value in enumValues)
                {
                    var name = value.ToEnumValueString();
                    var dbEntity = await context.Set<TEntity>().Where(p => p.Id == value).FirstOrDefaultAsync(cancelationToken);

                    if (dbEntity != null)
                    {
                        if (dbEntity.Name != name)
                        {
                            dbEntity.SetName(name);
                            context.Update(dbEntity);
                        }
                    }
                    else
                    {
                        if (Activator.CreateInstance(typeof(TEntity), new object[] { value, name }) is TEntity instance)
                        {
                            context.Add(instance);
                        }
                    }
                }

                await context.SaveChangesAsync(cancelationToken);
            }
        }
    }

}
