using Microsoft.Extensions.Configuration;
using WordLoop.Services;
using WordLoop.Settings;

namespace WordLoop.Extensions
{
    public static class ServiceConfigurationExtension
    {
        public static void ConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<ApiClientServiceSettings>(configuration.GetSection("APISettings"));

            services.AddScoped<ApiClientService>();
        }
    }
}
