using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using BlogMVVM.Dto;
using BlogMVVM.Models;
using MvvmHelpers.Commands;
using System.Threading.Tasks;

namespace BlogMVVM.ViewModels
{    
    public class AddPostPageViewModel : BaseViewModel
    {        
        public AddPostPageViewModel()
        {
            AddPostCommand = new AsyncCommand(AddPost);
        }

        private string titlePost;
        public string TitlePost
        {
            get => titlePost;
            set => SetProperty(ref titlePost, value);
        }

        private string contentPost;
        public string ContentPost
        {
            get => contentPost;
            set => SetProperty(ref contentPost, value);
        }

        private async Task AddPost()
        {
            CreatePostDto newPost = new CreatePostDto()
            {
                Blog = "hoennteam",
                Content = ContentPost,
                Title = TitlePost
            };
            await QueryManager.CreatePost(newPost.Content, newPost.Title, newPost.Blog);

            // GoToBack
            await Shell.Current.GoToAsync("..");
        }

        public AsyncCommand AddPostCommand { get; set; }


    }
}
