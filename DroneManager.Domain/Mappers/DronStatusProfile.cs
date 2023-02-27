using AutoMapper;
using DroneManager.Domain.DTOs;
using DroneManager.Domain.Models;

namespace DroneManager.Domain.Mappers
{
    public class DronStatusProfile : Profile
    {
        public DronStatusProfile()
        {
            CreateMap<DronStatus, EnumerationDTO>()
                .ForMember(x => x.Id, opt => opt.MapFrom(x => x.Id.GetHashCode()));
        }
    }
}
