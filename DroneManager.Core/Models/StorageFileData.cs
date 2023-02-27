using DroneManager.Core.Abstractions.Entities;

namespace DroneManager.Core.Models
{
    public class StorageFileData : Entity<int>
    {
        public string Url { get; set; }
        public long Size { get; set; }
        public bool IsImage { get; set; }
    }
}
