using AutoMapper;
using webapi.core.Entities;
using webapi.core.Models;

namespace webapi.MappingProfiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<UserEntity, User>();
        }
    }
}
