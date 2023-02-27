using AutoMapper;
using DroneManager.Application.Commands;
using DroneManager.Domain.DTOs;

namespace DroneManager.Application.Mappers
{
    public class DroneLoadCommandProfile : Profile
    {
        public DroneLoadCommandProfile()
        {
            CreateMap<DroneLoadDTO, DroneLoadCommand>();
        }
    }
}
