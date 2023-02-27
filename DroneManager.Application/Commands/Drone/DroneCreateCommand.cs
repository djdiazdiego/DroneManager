using DroneManager.Core.Abstractions.Commands;
using DroneManager.Domain.DTOs;
using DroneManager.Domain.Enums;

namespace DroneManager.Application.Commands
{
    public sealed class DroneCreateCommand : Command<int, DroneDTO>
    {
        public string SerialNumber { get; set; }
        public DroneModelValues ModelId { get; set; }
        public double Weight { get; set; }
        public decimal BatteryCapacity { get; set; }
        public DroneStatusValues StatusId { get; set; }
    }
}
