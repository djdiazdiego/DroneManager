using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DroneManager.Core.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    public static class TypeExtensions
    {
        /// <summary>
        /// Get the concrete types that implement or inherit from a given type
        /// </summary>
        /// <param name="type"></param>
        /// <param name="assemblies"></param>
        /// <returns></returns>
        public static Type[] GetConcreteTypes(this Type type, params Assembly[] assemblies)
        {
            var assemblyTypes = new List<Type>();

            if (assemblies == null || assemblies.Length == 0)
            {
                assemblyTypes.AddRange(type.Assembly.GetTypes());
            }
            else
            {
                foreach (var assembly in assemblies)
                {
                    assemblyTypes.AddRange(assembly.GetTypes());
                }
            }

            var types = !(type.IsGenericType && type.IsTypeDefinition) ?
                assemblyTypes.Where(t => t.IsClass && !t.IsAbstract && t.GetInterfaces().Contains(type)) :
                assemblyTypes.Where(t => t.IsClass && !t.IsAbstract && t.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == type));

            return types.ToArray();
        }
    }
}
