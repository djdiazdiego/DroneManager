using AutoMapper;
using DroneManager.Domain.DTOs;
using DroneManager.Domain.Models;

namespace DroneManager.Domain.Mappers
{
    public class DronModelProfile : Profile
    {
        public DronModelProfile()
        {
            CreateMap<DronModel, EnumerationDTO>()
                .ForMember(x => x.Id, opt => opt.MapFrom(x => x.Id.GetHashCode()));
        }
    }
}
