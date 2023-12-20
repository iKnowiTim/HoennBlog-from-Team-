using MvvmHelpers;
using MvvmHelpers.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using BlogMVVM.Dto;
using BlogMVVM.Models;

namespace BlogMVVM.ViewModels
{
    public class AddBlogPageViewModel : BaseViewModel
    {
        public AddBlogPageViewModel()
        {
            AddBlogCommand = new AsyncCommand(AddBlog);
        }

        private async Task AddBlog()
        {            
            await QueryManager.CreateBlog(Name, DisplayName);
            await Shell.Current.GoToAsync("..");
        }

        private string name;
        public string Name
        {
            get => name;
            set => SetProperty(ref name, value);
        }

        private string displayName;
        public string DisplayName
        {
            get => displayName;
            set => SetProperty(ref displayName, value);
        }

        public AsyncCommand AddBlogCommand { get; set; }

    }
}
