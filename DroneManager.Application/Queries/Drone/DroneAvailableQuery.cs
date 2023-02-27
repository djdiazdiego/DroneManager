using DroneManager.Core.Abstractions.Queries;
using DroneManager.Core.Wrappers;
using DroneManager.Domain.DTOs;

namespace DroneManager.Application.Queries
{
    public sealed class DroneAvailableQuery : IQuery<Response<IEnumerable<DroneDTO>>>
    {
        
    }
}
