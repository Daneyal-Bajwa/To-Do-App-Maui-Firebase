using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.ViewModel
{
    public partial class LoginViewModel : ViewModelBase
    {
        public LoginViewModel()
        {
            Initialize();
        }

        [RelayCommand]
        public static async void StartHomePage()
        {
            Application.Current.MainPage = new AppShell();
            // Ensure PhoneTabs is initialized
            var phoneTabs = Application.Current.MainPage.FindByName<TabBar>("PhoneTabs");
            if (DeviceInfo.Idiom == DeviceIdiom.Phone && phoneTabs != null)
            {
                Shell.Current.CurrentItem = phoneTabs;
                Shell.Current.FlyoutBehavior = FlyoutBehavior.Disabled;
            }
        }

        [RelayCommand]
        public async void CreateAccount()
        {

        }

    }
}
