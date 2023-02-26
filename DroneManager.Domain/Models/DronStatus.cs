using DroneManager.Core.Abstractions.Entities;
using DroneManager.Core.Abstractions.Entities.Interfaces;
using DroneManager.Domain.Enums;

namespace DroneManager.Domain.Models
{
    public class DronStatus : Enumeration<DronStatusValues>, INotRepository
    {
        protected DronStatus() { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="PeripheralDeviceStatusId"></param>
        /// <param name="name"></param>
        public DronStatus(DronStatusValues PeripheralDeviceStatusId, string name) : base(PeripheralDeviceStatusId, name) { }
    }
}
