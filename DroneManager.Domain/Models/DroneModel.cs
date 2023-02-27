using DroneManager.Core.Abstractions.Entities;
using DroneManager.Core.Abstractions.Entities.Interfaces;
using DroneManager.Domain.Enums;

namespace DroneManager.Domain.Models
{
    public class DroneModel : Enumeration<DroneModelValues>, INotRepository
    {
        protected DroneModel() { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="PeripheralDeviceStatusId"></param>
        /// <param name="name"></param>
        public DroneModel(DroneModelValues PeripheralDeviceStatusId, string name) : base(PeripheralDeviceStatusId, name) { }
    }
}
