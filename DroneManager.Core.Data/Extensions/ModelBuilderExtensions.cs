using DroneManager.Core.Abstractions.Entities.Interfaces;
using DroneManager.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace DroneManager.Core.Data.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    public static class ModelBuilderExtensions
    {
        /// <summary>
        /// Add entities and configurations to DbContext
        /// </summary>
        /// <param name="modelBuilder"></param>
        /// <param name="assembly"></param>
        public static void AddConfiguration(this ModelBuilder modelBuilder, Assembly assembly)
        {
            var types = typeof(IEntity).GetConcreteTypes();

            foreach (var type in types)
                modelBuilder.Entity(type);

            modelBuilder.ApplyConfigurationsFromAssembly(assembly, t => t.IsClass && !t.IsAbstract);
        }
    }
}
