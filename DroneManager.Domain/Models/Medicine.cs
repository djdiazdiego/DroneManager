using DroneManager.Core.Abstractions.Entities;
using DroneManager.Core.Models;

namespace DroneManager.Domain.Models
{
    public class Medicine : Entity<int>
    {
        public string Name { get; set; }
        public double Weight { get; set; }
        public string Code { get; set; }
        public int? StorageFileDataId { get; set; }

        public StorageFileData StorageFileData { get; set; }
    }
}
