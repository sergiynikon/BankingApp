using AutoMapper;
using Services.Helpers.MapperProfiles;

namespace Services.Helpers
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