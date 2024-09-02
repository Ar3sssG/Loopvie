using Loopvie.Pages.Signup;
using Loopvie.Pages.SpashScreen;
using Loopvie.Services;

namespace Loopvie
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new SplashPage();
        }

        protected override async void OnStart()
        {
            base.OnStart();

            await Task.Delay(1500);

            MainPage = new SignupPage();
        }
    }
}
