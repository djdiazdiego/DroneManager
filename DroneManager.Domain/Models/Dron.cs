using DroneManager.Core.Abstractions.Entities;
using DroneManager.Domain.Enums;

namespace DroneManager.Domain.Models
{
    public class Dron : Entity<int>
    {
        public string SerialNumber { get; set; }
        public DronModelValues ModelId { get; set; }
        public double Weight { get; set; }
        public decimal BatteryCapacity { get; set; }
        public DronStatusValues StatusId { get; set; }

        public DronModel Model { get; set; }
        public DronStatus Status { get; set; }

    }
}
