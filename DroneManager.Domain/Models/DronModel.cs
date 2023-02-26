using DroneManager.Core.Abstractions.Entities;
using DroneManager.Core.Abstractions.Entities.Interfaces;
using DroneManager.Domain.Enums;

namespace DroneManager.Domain.Models
{
    public class DronModel : Enumeration<DronModelValues>, INotRepository
    {
        protected DronModel() { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="PeripheralDeviceStatusId"></param>
        /// <param name="name"></param>
        public DronModel(DronModelValues PeripheralDeviceStatusId, string name) : base(PeripheralDeviceStatusId, name) { }
    }
}
