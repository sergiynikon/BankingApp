using AutoMapper;
using BankingApp.Services.Helpers.MapperProfiles;

namespace BankingApp.Services.Helpers
{
    public class MappingConfigHelper
    {
        public static IMapper GetMapper()
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new UserProfile());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            return mapper;
        }
    }
}