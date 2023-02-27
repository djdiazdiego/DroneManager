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
                .ReverseMap()
                .ForMember(x => x.Created, opt => opt.Ignore())
                .ForMember(x => x.LastModified, opt => opt.Ignore());
        }
    }
}
