using DroneManager.Core.Abstractions.Queries;
using DroneManager.Domain.DTOs;

namespace DroneManager.Application.Queries
{
    public sealed class DroneGetAllQuery : Query<IEnumerable<DroneDTO>>
    {
        
    }
}
