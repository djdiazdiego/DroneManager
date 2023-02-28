using DroneManager.Core.Abstractions.Commands;
using DroneManager.Domain.DTOs;
using Microsoft.AspNetCore.Http;

namespace DroneManager.Application.Commands
{
    public sealed class MedicineUpdateCommand : Command<int, MedicineDTO>
    {
        public string Name { get; set; }
        public double Weight { get; set; }
        public string Code { get; set; }

        public IFormFile? File { get; set; }
    }
}
