using DroneManager.Core.Abstractions.Dtos;
using DroneManager.Domain.Enums;

namespace DroneManager.Domain.DTOs
{
    public class DroneDTO : IDTO<int>
    {
        public int Id { get; set; }
        public string SerialNumber { get; set; }
        public DroneModelValues ModelId { get; set; }
        public string? ModelName { get; set; }
        public double Weight { get; set; }
        public decimal BatteryCapacity { get; set; }
        public DroneStatusValues StatusId { get; set; }
        public string? StatusName { get; set; }
    }
}
