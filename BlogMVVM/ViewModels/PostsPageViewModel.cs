using BlogMVVM.Models;
using BlogMVVM.Views;
using MvvmHelpers;
using MvvmHelpers.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace BlogMVVM.ViewModels
{
    public class PostsPageViewModel : BaseViewModel
    {
        public PostsPageViewModel()
        {
            GetPosts();
            DeleteCommand = new AsyncCommand<Post>(Delete);
            RefreshListViewCommand = new AsyncCommand(RefreshListView);
            SelectedCommand = new AsyncCommand<object>(Selected);
            GoToAddPageCommand = new AsyncCommand(GoToAddPage);
        }

        private async Task GoToAddPage()
        {
            await Shell.Current.GoToAsync($"{nameof(AddPostPage)}");
        }

        #region Methods

        private async Task Selected(object args)
        {                        
            if (SelectedPost == null)
                return;
            
            var route = $"{nameof(MyBlogPage)}?Id={SelectedPost.Id}";

            SelectedPost = null;
            await Shell.Current.GoToAsync(route);
        }

        private async Task RefreshListView()
        {
            if (!IsBusy)
            {
                IsBusy = true;
                Posts.Clear();
                Posts.AddRange(await QueryManager.GetPosts());

                await Task.Delay(1000);
                IsBusy = false;
            }
        }

        private async Task Delete(Post arg)
        {
            var post = new Post();
            await QueryManager.DeletePost(arg.Id);
            foreach (var item in Posts)
            {
                if (arg.Id == item.Id)
                {
                    post = item;
                }
            }
            Posts.Remove(post);
        }

        private async void GetPosts()
        {
            //Получение постов
            Posts = new ObservableRangeCollection<Post>(await QueryManager.GetPosts());
        }

        #endregion

        #region Properties

        private ObservableRangeCollection<Post> posts;
        public ObservableRangeCollection<Post> Posts
        {
            get => posts;
            set => SetProperty(ref posts, value);
        }

        private Post selectedPost;
        public Post SelectedPost
        {
            get => selectedPost;
            set => SetProperty(ref selectedPost, value);
        }


        #endregion

        #region Commands

        public AsyncCommand<Post> DeleteCommand { get; set; }
        public AsyncCommand RefreshListViewCommand { get; set; }
        public AsyncCommand<object> SelectedCommand { get; set; }
        public AsyncCommand GoToAddPageCommand { get; set; }

        #endregion
    }
}
