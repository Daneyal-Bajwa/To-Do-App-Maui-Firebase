using MauiApp1.Scripts;
using MauiApp1.Services;
using MauiApp1.View;
using Microsoft.Extensions.Configuration;

namespace MauiApp1
{
    public partial class App : Application
    {
        private readonly AuthConnection _authConnection;

        public App()
        {
            InitializeComponent();
            IConfiguration configuration = new ConfigurationBuilder()
                .AddUserSecrets<App>()
                .Build();
            _authConnection = new AuthConnection(configuration);

            LoginViewModel viewModel = new LoginViewModel(_authConnection);
            MainPage = new LoginPage(viewModel);
        }

        protected override void OnStart()
        {
            base.OnStart();

            // Set the background color of the app window (needed to change the color of the flyout)  
            Current.MainPage.BackgroundColor = (Color)Application.Current.Resources["Primary"];
        }
    }
}
