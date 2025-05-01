using Firebase.Auth;
using MauiApp1.Scripts;
using MauiApp1.Services;
using MauiApp1.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.ViewModel
{
    public partial class LoginViewModel : ViewModelBase
    {
        public FirebaseAuthClient _firebaseAuthClient => AuthConnection.Instance.firebaseAuthClient;
        [ObservableProperty] private LoginModel _loginModel = new();
        public LoginViewModel()
        {
            Initialize();
        }

        [RelayCommand]
        public static async void StartHomePage()
        {
            Application.Current.MainPage = new AppShell();
            // Ensure PhoneTabs is initialised
            var phoneTabs = Application.Current.MainPage.FindByName<TabBar>("PhoneTabs");
            if (DeviceInfo.Idiom == DeviceIdiom.Phone && phoneTabs != null)
            {
                Shell.Current.CurrentItem = phoneTabs;
                Shell.Current.FlyoutBehavior = FlyoutBehavior.Disabled;
            }
        }

        [RelayCommand]
        private async Task Login()
        {
            if (string.IsNullOrEmpty(LoginModel.Email) || string.IsNullOrEmpty(LoginModel.Password))
            {
                await Application.Current.MainPage.DisplayAlert("Login failed", "Please provide all the credentials.", "Okay");
                return;
            }
            try
            {
                var loginResult = await _firebaseAuthClient.SignInWithEmailAndPasswordAsync(LoginModel.Email, LoginModel.Password);
                if (!string.IsNullOrWhiteSpace(loginResult?.User?.Info?.Email))
                {
                    // Set the UserEmail property
                    UserService.Initialize(loginResult.User.Uid);

                    Application.Current.MainPage = new AppShell();
                    // Ensure PhoneTabs is initialized
                    var phoneTabs = Application.Current.MainPage.FindByName<TabBar>("PhoneTabs");
                    if (DeviceInfo.Idiom == DeviceIdiom.Phone && phoneTabs != null)
                    {
                        Shell.Current.CurrentItem = phoneTabs;
                        Shell.Current.FlyoutBehavior = FlyoutBehavior.Disabled;
                    }
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Login failed", "Please provide all the credentials.", "Okay");
                    return;
                }
            }
            catch (FirebaseAuthException ex) when (ex.Reason == AuthErrorReason.InvalidEmailAddress || ex.Reason == AuthErrorReason.WrongPassword)
            {
                await Application.Current.MainPage.DisplayAlert("Login failed", "Invalid email or password.", "Okay");
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Login failed", "Your login attempt was unsuccessful.", "Okay");
                //await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "Okay");
            }
        }
        [RelayCommand]
        private async Task NavigateToSignUp()
        {
            Application.Current.MainPage = new SignUpPage();
        }

        [RelayCommand]
        public void Bypass()
        {
            LoginModel.Email = "test@gmail.com";
            LoginModel.Password = "password";
            Login();
        }
    }
}
