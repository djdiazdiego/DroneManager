using AutoMapper;
using DroneManager.Application.Commands;
using DroneManager.Domain.DTOs;
using DroneManager.Domain.Models;

namespace DroneManager.Application.Mappers
{
    public class MedicineCreateCommandProfile : Profile
    {
        public MedicineCreateCommandProfile()
        {
            CreateMap<MedicineFileDTO, MedicineCreateCommand>();

            CreateMap<MedicineCreateCommand, Medicine>()
                .ForMember(x => x.Created, opt => opt.Ignore())
                .ForMember(x => x.LastModified, opt => opt.Ignore())
                .ForMember(x => x.Drone, opt => opt.Ignore())
                .ForMember(x => x.DroneId, opt => opt.Ignore())
                .ForMember(x => x.StorageFileData, opt => opt.Ignore())
                .ForMember(x => x.StorageFileDataId, opt => opt.Ignore());
        }
    }
}
