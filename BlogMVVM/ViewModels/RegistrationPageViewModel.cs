using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Text;
using BlogMVVM.Models;
using MvvmHelpers.Commands;
using System.Windows.Input;
using System.Threading.Tasks;
using Xamarin.Forms;
using BlogMVVM.Exceptions;

namespace BlogMVVM.ViewModels
{
    public class RegistrationPageViewModel : BaseViewModel
    {
        public RegistrationPageViewModel()
        {
            RegistrationCommand = new AsyncCommand(Registration);
        }

        private async Task Registration()
        {
            try {
                await QueryManager.RegisterUser(Name, Email, Password);
                await Shell.Current.GoToAsync("..");
                await Shell.Current.DisplayAlert("Message", "Registered successfull!", "Ok");
            }
            catch (ArgumentNullException e)
            {
                await Shell.Current.DisplayAlert("Message",$"{e.Message}", "Ok");
            }
            catch (HoennApiException e) {
                string messages = String.Join("\n", e.ErrorDto.Messages);
                await Shell.Current.DisplayAlert("Message", $"{messages}", "Ok");
            }
            catch (Exception) {
                await Shell.Current.DisplayAlert("Unknown exception", $"Server error", "Ok");
            }
        }

        private bool IsRegister()
        {
            return Name != null && Name.Length > 3 && Name.Length < 10 &&
                Email != null && Email.Contains("@") && 
                Password != null && Password.Length > 8;
        }

        private string name;
        public string Name
        {
            get => name;
            set => SetProperty(ref name, value);
        }

        private string email;
        public string Email
        {
            get => email;
            set => SetProperty(ref email, value);
        }

        private string password;
        public string Password
        {
            get => password;
            set => SetProperty(ref password, value);
        }

        public AsyncCommand RegistrationCommand { get; set; }
    }
}
