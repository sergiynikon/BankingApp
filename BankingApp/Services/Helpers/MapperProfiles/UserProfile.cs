using AutoMapper;
using Data.Entities;
using DTO;

namespace Services.Helpers.MapperProfiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<LoginDTO, User>();
            CreateMap<RegisterDTO, User>();
        }
    }
}