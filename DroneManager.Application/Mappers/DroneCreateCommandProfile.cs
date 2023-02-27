using AutoMapper;
using DroneManager.Application.Commands;
using DroneManager.Domain.DTOs;
using DroneManager.Domain.Models;

namespace DroneManager.Application.Mappers
{
    public class DroneCreateCommandProfile : Profile
    {
        public DroneCreateCommandProfile()
        {
            CreateMap<DroneDTO, DroneCreateCommand>();

            CreateMap<DroneCreateCommand, Drone>()
                .ForMember(x => x.Created, opt => opt.Ignore())
                .ForMember(x => x.LastModified, opt => opt.Ignore())
                .ForMember(x => x.Status, opt => opt.Ignore())
                .ForMember(x => x.Model, opt => opt.Ignore())
                .ForMember(x => x.Medicines, opt => opt.Ignore());
        }
    }
}
