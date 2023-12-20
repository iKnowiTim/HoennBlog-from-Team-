using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using BlogMVVM.Models;
using MvvmHelpers.Commands;
using System.Threading.Tasks;

namespace BlogMVVM.ViewModels
{    
    public class EditProfilePageViewModel : BaseViewModel
    {
        public EditProfilePageViewModel()
        {
            GetUser();
            PatchUserCommand = new AsyncCommand<User>(PatchUser);
        }

        private async Task PatchUser(User arg)
        {
            try
            {
                await QueryManager.PatchUser(User.UserName, User.Email, User.Role);
                await Shell.Current.GoToAsync("..");
            }
            catch (ArgumentNullException e)
            {
                await Shell.Current.DisplayAlert("Message",$"{e.Message}", "OK");
            }            
            catch(Exception e)
            {
                await Shell.Current.DisplayAlert("Message", $"{e.Message}", "OK");
            }
        }

        private async void GetUser()
        {
            User = await QueryManager.GetUser();
        }

        private User user;
        public User User
        {
            get => user;
            set => SetProperty(ref user, value);
        }


        public AsyncCommand<User> PatchUserCommand { get; set; }
    }
}
