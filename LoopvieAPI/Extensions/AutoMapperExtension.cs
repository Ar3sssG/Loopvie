using ASBusinessLogic.MapperProfiles;
using AutoMapper;
using LoopvieBusinessLogic.MapperProfiles;

namespace LoopvieAPI.Extensions
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
