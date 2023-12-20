using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Text;
using BlogMVVM.Models;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BlogMVVM.ViewModels
{
    public class UsersPageViewModel : BaseViewModel
    {
        public UsersPageViewModel()
        {
            GetUsers();
        }
        
        #region Property

        ObservableRangeCollection<User> users;
        public ObservableRangeCollection<User> Users
        {
            get => users;
            set => SetProperty(ref users, value);            
        }

        #endregion


        #region Methods
        //Получение списка пользователей, добавляет за раз один элемент (Оптимизация)

        private async void GetUsers()
        {
            Users = new ObservableRangeCollection<User>(await QueryManager.GetUsers());
        }
        #endregion

    }
}
