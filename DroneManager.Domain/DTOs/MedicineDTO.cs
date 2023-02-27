using DroneManager.Core.Abstractions.Dtos;
using DroneManager.Core.DTOs;

namespace DroneManager.Domain.DTOs
{
    public class MedicineDTO : IDTO<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Weight { get; set; }
        public string Code { get; set; }
        public int? StorageFileDataId { get; set; }

        public StorageFileDataDTO StorageFileData { get; set; }
    }
}
