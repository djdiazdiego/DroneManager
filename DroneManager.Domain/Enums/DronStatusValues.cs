using System.Runtime.Serialization;

namespace DroneManager.Domain.Enums
{
    public enum DronStatusValues
    {
        IDLE = 1,
        CHARGING = 2,
        LOADED = 3,
        [EnumMember(Value = "DELIVERING LOAD")]
        DELIVERING_LOAD = 4,
        [EnumMember(Value = "DELIVERED LOAD")]
        DELIVERED_LOAD = 5,
        RETURNING = 6
    }
}
