using BlogMVVM.Models;
using BlogMVVM.Views;
using MvvmHelpers;
using MvvmHelpers.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BlogMVVM.ViewModels
{
    [QueryProperty(nameof(Name), nameof(Name))]
    public class BlogPageViewModel : BaseViewModel
    {
        public BlogPageViewModel()
        {
            RefreshListViewCommand = new AsyncCommand(RefreshListView);
            DeleteCommand = new AsyncCommand<object>(Delete);
            SelectedCommand = new AsyncCommand<object>(Selected);
        }

        private async Task Selected(object arg)
        {
            if (SelectedPost == null)
                return;

            var route = $"{nameof(MyBlogPage)}?Id={SelectedPost.Id}";
            SelectedPost = null;
            await Shell.Current.GoToAsync(route);
        }

        private async Task Delete(object args)
        {
            if (args == null)
                return;

            Post post = args as Post;            
            await QueryManager.DeletePost(post.Id);
            foreach (var item in Posts)
            {
                if (post == item)
                {
                    post = item;
                }
            }
            Posts.Remove(post);
        }

        private async Task RefreshListView()
        {
            if (!IsBusy)
            {
                IsBusy = true;
                Posts.Clear();
                Posts.AddRange(await QueryManager.GetBlogPosts(Name));

                await Task.Delay(1000);

                IsBusy = false;
            }
        }

        private string name;
        public string Name
        {
            get => name;
            set
            {
                name = value;                
                OnPropertyChanged();
                GetBlogPosts();
            }
        }

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


        private async void GetBlogPosts()
        {
            Posts = new ObservableRangeCollection<Post>(await QueryManager.GetBlogPosts(Name));
        }

        public AsyncCommand RefreshListViewCommand { get; set; }
        public AsyncCommand<object> DeleteCommand { get; set; }
        public AsyncCommand<object> SelectedCommand { get; set; }

    }
}
