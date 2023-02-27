using AutoMapper;
using DroneManager.Domain.DTOs;
using DroneManager.Domain.Models;

namespace DroneManager.Domain.Mappers
{
    public class DronProfile : Profile
    {
        public DronProfile()
        {
            CreateMap<Dron, DronDTO>()
                .ForMember(x => x.StatusName, opt => opt.MapFrom(x => x.Status.Name))
                .ForMember(x => x.ModelName, opt => opt.MapFrom(x => x.Model.Name))
                .ReverseMap()
                .ForMember(x => x.Model, opt => opt.Ignore())
                .ForMember(x => x.Status, opt => opt.Ignore());
        }
    }
}
