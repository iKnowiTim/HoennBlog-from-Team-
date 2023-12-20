using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Text;
using BlogMVVM.Models;
using System.Threading.Tasks;
using MvvmHelpers.Commands;
using BlogMVVM.Views;

namespace BlogMVVM.ViewModels
{
    public class ProfilePageViewModel : BaseViewModel
    {
        public ProfilePageViewModel()
        {
            GetUser();
            GetUsers();
            EditProfileCommand = new AsyncCommand(EditProfile);            
        }        

        private async Task EditProfile()
        {
            await AppShell.Current.GoToAsync($"{nameof(EditProfilePage)}");
        }

        private async void GetUser()
        {
            object name;
            if (App.Current.Properties.TryGetValue("UserName", out name))
            {
                if ((string)name == "Guest")
                {
                    User = new User()
                    {
                        UserName = Secret.CurrentUser,
                        Email = "None",
                        Role = "USER"
                    };
                }
                else
                {
                    User = await QueryManager.GetUser();
                }
            }
            
        }

        ObservableRangeCollection<User> users;
        public ObservableRangeCollection<User> Users
        {
            get => users;
            set => SetProperty(ref users, value);
        }

        private User user;
        public User User
        {
            get => user;
            set => SetProperty(ref user, value);
        }

        private async void GetUsers()
        {
            Users = new ObservableRangeCollection<User>(await QueryManager.GetUsers());
        }

        public AsyncCommand EditProfileCommand { get; set; }        
    }
}
