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
    public partial class SignUpViewModel : ViewModelBase
    {
        public FirebaseAuthClient _firebaseAuthClient => AuthConnection.Instance.firebaseAuthClient;

        [ObservableProperty] private SignUpModel _signUpModel = new();
        public SignUpViewModel()
        {
            Initialize();
        }

        [RelayCommand]
        private async Task SignUp()
        {
            if (string.IsNullOrEmpty(SignUpModel.Username) || string.IsNullOrEmpty(SignUpModel.Email) || string.IsNullOrEmpty(SignUpModel.Password)) {
                await Application.Current.MainPage.DisplayAlert("Sign up failed", "Please provide all the credentials.", "Okay");
                return;
            }
            try
            {
                var signUpResult = await _firebaseAuthClient.CreateUserWithEmailAndPasswordAsync(SignUpModel.Email, SignUpModel.Password, SignUpModel.Username);
                if (!string.IsNullOrWhiteSpace(signUpResult?.User?.Info?.Email))
                {
                    // Set the UserEmail property
                    UserService.Initialize(signUpResult.User.Uid);

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
                    await Application.Current.MainPage.DisplayAlert("Sign up failed", "Please provide all the credentials.", "Okay");
                    return;
                }
            }
            catch (FirebaseAuthException ex) when (ex.Reason == AuthErrorReason.InvalidEmailAddress || ex.Reason == AuthErrorReason.WrongPassword)
            {
                await Application.Current.MainPage.DisplayAlert("Sign up failed", "Invalid email or password.", "Okay");
            }
            catch (FirebaseAuthException ex) when (ex.Reason == AuthErrorReason.AccountExistsWithDifferentCredential)
            {
                await Application.Current.MainPage.DisplayAlert("Sign up failed", "Account already exists.", "Okay");
            }
            catch (FirebaseAuthException ex) when (ex.Reason == AuthErrorReason.EmailExists)
            {
                await Application.Current.MainPage.DisplayAlert("Sign up failed", "Email already exists.", "Okay");
            }
            catch (FirebaseAuthException ex) when (ex.Reason == AuthErrorReason.WeakPassword)
            {
                await Application.Current.MainPage.DisplayAlert("Sign up failed", "The password is weak. Must be at least 6 characters.", "Okay");
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Sign up failed", "Invalid credentials.", "Okay");
                // await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "Okay");
            }
        }

        [RelayCommand]
        private async Task NavigateToLogin()
        {
            Application.Current.MainPage = new LoginPage();
        }
    }
}
