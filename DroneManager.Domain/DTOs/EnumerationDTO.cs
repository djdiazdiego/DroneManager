using DroneManager.Core.Abstractions.Dtos;
using System;

namespace DroneManager.Domain.DTOs
{
    public class EnumerationDTO : IDTO<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTimeOffset Created { get; set; }
        public DateTimeOffset? LastModified { get; set; }
    }
}
