using DroneManager.Core.Abstractions.Dtos;

namespace DroneManager.Domain.DTOs
{
    public class EnumerationDTO : IDTO<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
