using Loopvie.ViewModels;

namespace Loopvie.Extensions
{
    public static class ViewModelsExtension
    {
        public static void AddViewModels(this IServiceCollection services)
        {
            services.AddTransient<DetailViewModel>();
            services.AddTransient<LoginViewModel>();
            services.AddTransient<MainViewModel>();
        }
    }
}
