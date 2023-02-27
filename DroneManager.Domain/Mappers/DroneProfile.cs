using AutoMapper;
using DroneManager.Core.Extensions;
using DroneManager.Domain.DTOs;
using DroneManager.Domain.Models;

namespace DroneManager.Domain.Mappers
{
    public class DroneProfile : Profile
    {
        public DroneProfile()
        {
            CreateMap<Drone, DroneDTO>()
                .ForMember(x => x.StatusName, opt => opt.MapFrom(x => x.Status != null ? x.Status.Name : x.StatusId.ToEnumValueString()))
                .ForMember(x => x.ModelName, opt => opt.MapFrom(x => x.Model != null ? x.Model.Name : x.ModelId.ToEnumValueString()))
                .ReverseMap()
                .ForMember(x => x.Model, opt => opt.Ignore())
                .ForMember(x => x.Status, opt => opt.Ignore());
        }
    }
}
