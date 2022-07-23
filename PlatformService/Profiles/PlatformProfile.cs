
using AutoMapper;
using PlatformService.DTO;
using PlatformService.Models;

namespace PlatformService.Profiles
{
    public class PlatformProfile : Profile
    {
        public PlatformProfile()
        {
            CreateMap<Platform,PlatformReadDTO>();
            CreateMap<PlatformCreatDTO, Platform>();
        }
    }
}