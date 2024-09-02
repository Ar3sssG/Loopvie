using Loopvie.Pages.Signin;

namespace Loopvie.Extensions
{
    public static class PagesExtension
    {
        public static void AddPages(this IServiceCollection services)
        {
            services.AddSingleton<MainPage>();
            services.AddSingleton<SigninPage>();
            services.AddSingleton<DetailPage>();
        }
    }
}
