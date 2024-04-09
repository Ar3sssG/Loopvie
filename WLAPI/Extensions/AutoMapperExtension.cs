using ASBusinessLogic.MapperProfiles;
using AutoMapper;

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
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
        }
    }
}
