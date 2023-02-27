using DroneManager.Core.Abstractions.Dtos;

namespace DroneManager.Domain.DTOs
{
    public class DroneMedicationWeightDTO : IDTO<int>
    {
        public int Id { get; set; }
        public double Weight { get; set; }
        public double MedicationWeight { get; set; }
    }
}
