using System;
using System.Threading;
using System.Threading.Tasks;

namespace DroneManager.Core.Abstractions.Persistence
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISeed
    {
        /// <summary>
        /// Load seed data
        /// </summary>
        /// <param name="provider"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task SeedAsync(IServiceProvider provider, CancellationToken cancellationToken);
    }
}
