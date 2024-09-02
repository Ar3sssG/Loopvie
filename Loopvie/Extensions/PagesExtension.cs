using Loopvie.Pages.Login;

namespace Loopvie.Extensions
{
    public static class PagesExtension
    {
        public static void AddPages(this IServiceCollection services)
        {
            services.AddSingleton<MainPage>();
            services.AddSingleton<LoginPage>();
            services.AddSingleton<DetailPage>();
        }
    }
}
