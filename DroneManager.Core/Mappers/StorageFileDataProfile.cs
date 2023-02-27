using AutoMapper;
using DroneManager.Core.DTOs;
using DroneManager.Core.Models;

namespace DroneManager.Core.Mappers
{
    public class StorageFileDataProfile : Profile
    {
        public StorageFileDataProfile()
        {
            CreateMap<StorageFileData, StorageFileDataDTO>()
                .ReverseMap();
        }
    }
}
