using DroneManager.Core.Abstractions.Dtos;
using System.Collections.Generic;

namespace DroneManager.Domain.DTOs
{
    public class DroneLoadDTO : IDTO<int>
    {
        public int Id { get; set; }
        public List<int> MedicineIds { get; set; }
    }
}
