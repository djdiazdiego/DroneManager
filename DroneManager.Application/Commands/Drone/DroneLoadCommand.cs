using DroneManager.Core.Abstractions.Commands;
using DroneManager.Domain.DTOs;

namespace DroneManager.Application.Commands
{
    public sealed class DroneLoadCommand : Command<int, DroneLoadDTO>
    {
        public List<int> MedicineIds { get; set; }
    }
}
