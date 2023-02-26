using DroneManager.Core.Abstractions.Entities;
using DroneManager.Domain.Enums;

namespace DroneManager.Domain.Models
{
    public class Dron : Entity<int>
    {
        public string SerialNumber { get; set; }
        public ModelValues Model { get; set; }
        public double Weight { get; set; }
        public decimal BatteryCapacity { get; set; }
        public DronStatusValues Status { get; set; }

    }
}
