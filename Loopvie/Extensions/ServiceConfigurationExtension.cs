using Microsoft.Extensions.Configuration;
using Loopvie.Services;
using Loopvie.Settings;

namespace Loopvie.Extensions
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
