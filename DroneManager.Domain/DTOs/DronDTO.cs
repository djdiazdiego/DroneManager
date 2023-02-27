using DroneManager.Core.Abstractions.Dtos;
using DroneManager.Domain.Enums;
using System;

namespace DroneManager.Domain.DTOs
{
    public class DronDTO : IDTO<int>
    {
        public int Id { get; set; }
        public string SerialNumber { get; set; }
        public DronModelValues ModelId { get; set; }
        public string ModelName { get; set; }
        public double Weight { get; set; }
        public decimal BatteryCapacity { get; set; }
        public DronStatusValues StatusId { get; set; }
        public string StatusName { get; set; }
        public DateTimeOffset Created { get; set; }
        public DateTimeOffset? LastModified { get; set; }
    }
}
