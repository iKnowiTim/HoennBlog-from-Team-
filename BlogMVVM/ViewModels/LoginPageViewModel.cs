using MvvmHelpers;
using MvvmHelpers.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BlogMVVM.Views;
using Xamarin.Forms;
using BlogMVVM.Models;
using BlogMVVM.Exceptions;

namespace BlogMVVM.ViewModels
{
    public class LoginPageViewModel : BaseViewModel
    {
        public LoginPageViewModel()
        {
            GoToRegistrationPageCommand = new AsyncCommand(GoToRegistrationPage);            

            Me();
            

            LoginCommand = new AsyncCommand(SignIn);
        }        

        private async void Me()
        {
            object token = "";
            if (App.Current.Properties.TryGetValue("Token", out token))
            {
                try
                {
                    await QueryManager.GetMeAccount(token);
                    Secret.CurrentUser = (string)App.Current.Properties["UserName"];
                    await Shell.Current.GoToAsync($"//{nameof(PostsPage)}");
                }
                catch (Exception e)
                {
                    App.Current.Properties.Remove("Token");                    
                    App.Current.Properties["UserName"] = "Guest";
                    await Shell.Current.DisplayAlert("Message",$"{e.Message}","Ok");
                }                                
            }
        }

        async Task SignIn()
        {
            try
            {
                await QueryManager.SignIn(Login, Password);

                App.Current.Properties["Token"] = Secret.Token;
                App.Current.Properties["UserName"] = Secret.CurrentUser;

                await Shell.Current.GoToAsync($"//{nameof(PostsPage)}");
            }
            catch (ArgumentNullException e)
            {
                await Shell.Current.DisplayAlert("Message", $"{e.Message}", "Ok");
            }
            catch (HoennApiException e)
            {
                await Shell.Current.DisplayAlert("Meesage", $"{e.ErrorDto.Messages[0]}", "Ok");
            }
            catch (Exception)
            {
                await Shell.Current.DisplayAlert("Message", "Server Error", "Ok");
            }
        }                            

        private async Task GoToRegistrationPage()
        {
            await Shell.Current.GoToAsync($"//{nameof(LoginPage)}/{nameof(RegistrationPage)}");
        }

        private string login;
        public string Login
        {
            get => login;
            set => SetProperty(ref login, value);
        }

        private string password;
        public string Password
        {
            get => password;
            set => SetProperty(ref password, value);
        }        

        public AsyncCommand GoToRegistrationPageCommand { get; set; }
        public AsyncCommand LoginCommand { get; set; }
        
    }
}
