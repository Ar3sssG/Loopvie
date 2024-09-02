using Loopvie.Pages.Signin;
using Loopvie.Pages.Signup;

namespace Loopvie
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(DetailPage),typeof(DetailPage));
            Routing.RegisterRoute(nameof(SigninPage), typeof(SigninPage));
            Routing.RegisterRoute(nameof(SignupPage), typeof(SignupPage));
        }
    }
}
