using DroneManager.Core.Data.Seeds;
using DroneManager.Domain.Models;
using DroneManager.Infrastructure.Contexts;

namespace DroneManager.Infrastructure.Seeds
{
    public sealed class DronModelSeed : EnumerationSeed<DronModel, DbContextWrite>
    {
    }
}
