using AutoMapper;
using DroneManager.Domain.DTOs;
using DroneManager.Domain.Models;

namespace DroneManager.Domain.Mappers
{
    public class DroneModelProfile : Profile
    {
        public DroneModelProfile()
        {
            CreateMap<DroneModel, EnumerationDTO>()
                .ForMember(x => x.Id, opt => opt.MapFrom(x => x.Id.GetHashCode()));
        }
    }
}
