using WordLoop.Pages.Login;

namespace WordLoop.Extensions
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
