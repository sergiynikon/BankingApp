using AutoMapper;
using BankingApp.Data.Entities;
using BankingApp.DataTransfer;

namespace BankingApp.Services.Helpers.MapperProfiles
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