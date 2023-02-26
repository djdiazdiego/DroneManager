using System;

namespace DroneManager.Core.Abstractions.Entities.Interfaces
{
    public interface IEntity : ICloneable
    {
        /// <summary>
        /// Entity identifier
        /// </summary>
        object Id { get; }

        /// <summary>
        /// Creation date
        /// </summary>
        DateTimeOffset Created { get; }

        /// <summary>
        /// Modification date
        /// </summary>
        DateTimeOffset? LastModified { get; }

        /// <summary>
        /// Indicate is not initialized yet
        /// </summary>
        /// <returns></returns>
        bool IsTransient();

        /// <summary>
        /// Set creation date
        /// </summary>
        /// <param name="date"></param>
        void SetCreatedDate(DateTimeOffset date);

        /// <summary>
        /// Set modification date
        /// </summary>
        /// <param name="date"></param>
        void SetLastModified(DateTimeOffset date);
    }
}
