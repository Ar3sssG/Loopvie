using WLBusinessLogic.Interfaces;
using WLBusinessLogic.Managers;

namespace WLAPI.Extensions
{
    public static class ManagerServicesExtension
    {
        public static void AddManagerServices(this IServiceCollection services)
        {
            services.AddScoped<IIdentityManager, IdentityManager>();
        }
    }
}
