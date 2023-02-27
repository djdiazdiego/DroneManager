using AutoMapper;
using DroneManager.Application.Commands;
using DroneManager.Domain.DTOs;
using DroneManager.Domain.Models;

namespace DroneManager.Application.Mappers
{
    public class DroneUpdateCommandProfile : Profile
    {
        public DroneUpdateCommandProfile()
        {
            CreateMap<DroneDTO, DroneUpdateCommand>();

            CreateMap<DroneUpdateCommand, Drone>()
                .ForMember(x => x.Created, opt => opt.Ignore())
                .ForMember(x => x.LastModified, opt => opt.Ignore())
                .ForMember(x => x.Status, opt => opt.Ignore())
                .ForMember(x => x.Model, opt => opt.Ignore())
                .ForMember(x => x.Medicines, opt => opt.Ignore());
        }
    }
}
