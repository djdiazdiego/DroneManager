using AutoMapper;
using DroneManager.Domain.DTOs;
using DroneManager.Domain.Models;

namespace DroneManager.Domain.Mappers
{
    public class MedicineProfile : Profile
    {
        public MedicineProfile()
        {
            CreateMap<Medicine, MedicineDTO>();
        }
    }
}
