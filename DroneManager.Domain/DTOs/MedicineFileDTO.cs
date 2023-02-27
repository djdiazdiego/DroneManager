using DroneManager.Core.Abstractions.Dtos;
using Microsoft.AspNetCore.Http;

namespace DroneManager.Domain.DTOs
{
    public class MedicineFileDTO : IDTO<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Weight { get; set; }
        public string Code { get; set; }

        public IFormFile? File { get; set; }
    }
}
