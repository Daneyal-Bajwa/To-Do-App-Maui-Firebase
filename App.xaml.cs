using MauiApp1.View;

namespace MauiApp1
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();

            // Ensure PhoneTabs is initialized
            var phoneTabs = MainPage.FindByName<TabBar>("PhoneTabs");
            if (DeviceInfo.Idiom == DeviceIdiom.Phone && phoneTabs != null)
            {
                Shell.Current.CurrentItem = phoneTabs;
                Shell.Current.FlyoutBehavior = FlyoutBehavior.Disabled;
            }



        }

        protected override void OnStart()
        {
            base.OnStart();

            // Set the background color of the app window (needed to change the colour of the flyout)
            Current.MainPage.BackgroundColor = (Color)Application.Current.Resources["Primary"];
        }
    }
}
