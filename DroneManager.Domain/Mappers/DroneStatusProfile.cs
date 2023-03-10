using AutoMapper;
using DroneManager.Domain.DTOs;
using DroneManager.Domain.Models;

namespace DroneManager.Domain.Mappers
{
    public class DroneStatusProfile : Profile
    {
        public DroneStatusProfile()
        {
            CreateMap<DroneStatus, EnumerationDTO>()
                .ForMember(x => x.Id, opt => opt.MapFrom(x => x.Id.GetHashCode()));
        }
    }
}
