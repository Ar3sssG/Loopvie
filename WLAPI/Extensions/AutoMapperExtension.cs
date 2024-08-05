using ASBusinessLogic.MapperProfiles;
using AutoMapper;
using WLBusinessLogic.MapperProfiles;

namespace WLAPI.Extensions
{
    public static class AutoMapperExtension
    {
        public static void ConfigureAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(Program));

            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile<PagedListProfile>();
                mc.AddProfile<WordProfile>();
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
        }
    }
}
