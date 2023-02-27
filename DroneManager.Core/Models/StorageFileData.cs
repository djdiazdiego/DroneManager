using DroneManager.Core.Abstractions.Entities;

namespace DroneManager.Core.Models
{
    public class StorageFileData : Entity<int>
    {
        public string Key { get; set; }
        public string FileName { get; set; }
        public string Url { get; set; }
        public long Size { get; set; }
        public string Extension { get; set; }
        public bool IsImage { get; set; }
    }
}
