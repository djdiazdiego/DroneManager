using DroneManager.Core.Abstractions.Dtos;

namespace DroneManager.Domain.DTOs
{
    public class DroneBatteryDTO : IDTO<int>
    {
        public int Id { get; set; }
        public decimal BatteryCapacity { get; set; }
    }
}
