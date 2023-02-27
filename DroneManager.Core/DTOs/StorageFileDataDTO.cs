using DroneManager.Core.Abstractions.Dtos;
using System;

namespace DroneManager.Core.DTOs
{
    public class StorageFileDataDTO : IDTO<int>
    {
        public int Id { get; set; }
        public string Key { get; set; }
        public string FileName { get; set; }
        public string Url { get; set; }
        public long Size { get; set; }
        public string Extension { get; set; }
        public bool IsImage { get; set; }
        public DateTimeOffset Created { get; set; }
        public DateTimeOffset? LastModified { get; set; }
    }
}
