using DroneManager.Core.Abstractions.Entities;
using DroneManager.Core.Abstractions.Entities.Interfaces;
using DroneManager.Domain.Enums;

namespace DroneManager.Domain.Models
{
    public class DroneStatus : Enumeration<DroneStatusValues>, INotRepository
    {
        protected DroneStatus() { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="PeripheralDeviceStatusId"></param>
        /// <param name="name"></param>
        public DroneStatus(DroneStatusValues PeripheralDeviceStatusId, string name) : base(PeripheralDeviceStatusId, name) { }
    }
}
