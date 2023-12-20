using BlogMVVM.Views;
using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using BlogMVVM.Dto;
using BlogMVVM.Models;
using MvvmHelpers.Commands;

namespace BlogMVVM.ViewModels
{
    // Долго инициализирует
    [QueryProperty(nameof(Id), nameof(Id))]  
    public class PostPageViewModel : BaseViewModel
    {        
        public PostPageViewModel()
        {
            PatchPostCommand = new AsyncCommand(PatchPost);                       
        }        

        private async Task PatchPost()
        {            
            await QueryManager.PatchPost(TitlePost, Content, true, Convert.ToInt32(Id));
            await Shell.Current.GoToAsync("..");
        }

        public Post Post { get; set; }

        string id;
        public string Id 
        { 
            get => id; 
            set 
            {
                // Исправить!
                id = value;
                GetPost();             
                OnPropertyChanged();
            } 
        }

        async void GetPost()
        {
            Post = await QueryManager.GetPost(Convert.ToInt32(Id));
            TitlePost = Post.Title;
            Content = Post.Content;
            DisplayName = Post.Blog.DisplayName;
        }

        private string titlePost;
        public string TitlePost
        {
            get => titlePost;
            set => SetProperty(ref titlePost, value);
        }

        private string content;
        public string Content
        {
            get => content;
            set => SetProperty(ref content, value);
        }

        private string displayName;
        public string DisplayName
        {
            get => displayName;
            set => SetProperty(ref displayName, value);
        }


        public AsyncCommand PatchPostCommand { get; set; }

    }
}
