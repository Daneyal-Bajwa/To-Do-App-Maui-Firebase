using MauiApp1.Services;
using MauiApp1.View;

namespace MauiApp1
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new LoginPage();
        }

        protected override void OnStart()
        {
            base.OnStart();
            
            // Set the background color of the app window (needed to change the colour of the flyout)
            Current.MainPage.BackgroundColor = (Color)Application.Current.Resources["Primary"];
        }
    }
}
