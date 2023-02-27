using DroneManager.Core.Abstractions.Entities;
using DroneManager.Domain.Enums;
using System.Collections.Generic;

namespace DroneManager.Domain.Models
{
    public class Drone : Entity<int>
    {
        public string SerialNumber { get; set; }
        public DroneModelValues ModelId { get; set; }
        public double Weight { get; set; }
        public decimal BatteryCapacity { get; set; }
        public DroneStatusValues StatusId { get; set; }

        public DroneModel Model { get; set; }
        public DroneStatus Status { get; set; }

        public ICollection<Medicine> Medicines { get; set; }

    }
}
