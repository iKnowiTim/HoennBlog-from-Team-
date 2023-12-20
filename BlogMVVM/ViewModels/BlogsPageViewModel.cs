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
    public class BlogsPageViewModel : BaseViewModel
    {
        public BlogsPageViewModel()
        {
            GetBlogs();
            SelectedCommand = new AsyncCommand<object>(Selected);
            RefreshListViewCommand = new AsyncCommand(RefreshListView);
            DeleteCommand = new AsyncCommand<object>(Delete);
            GoToAddBlogPageCommand = new AsyncCommand(GoToAddBlogPage);
        }

        private async Task GoToAddBlogPage()
        {
            await Shell.Current.GoToAsync($"{nameof(AddBlogPage)}");
        }

        private async Task Delete(object arg)
        {
            var blog = arg as Blog;
            await QueryManager.DeleteBlog(blog);
            foreach (var item in Blogs)
            {
                if (blog.Id == item.Id)
                {
                    blog = item;
                }
            }
            Blogs.Remove(blog);
        }

        private async Task RefreshListView()
        {
            if (!IsBusy)
            {
                IsBusy = true;
                Blogs.Clear();
                Blogs.AddRange(await QueryManager.GetBlogs());

                await Task.Delay(1000);
                IsBusy = false;
            }
        }

        private async Task Selected(object args)
        {            
            if (SelectedBlog == null)
                return;
            
            var route = $"{nameof(ProgramBlogPage)}?Name={SelectedBlog.Name}";
            SelectedBlog = null;
            await Shell.Current.GoToAsync(route);
        }

        private ObservableRangeCollection<Blog> blogs;
        public ObservableRangeCollection<Blog> Blogs
        {
            get => blogs;
            set => SetProperty(ref blogs, value);
        }

        private Blog selectedBlog;
        public Blog SelectedBlog
        {
            get => selectedBlog;
            set => SetProperty(ref selectedBlog, value);
        }        

        private async void GetBlogs()
        {
            Blogs = new ObservableRangeCollection<Blog>(await QueryManager.GetBlogs());
        }

        public AsyncCommand<object> SelectedCommand { get; set; }
        public AsyncCommand RefreshListViewCommand { get; set; }
        public AsyncCommand<object> DeleteCommand { get; set; }
        public AsyncCommand GoToAddBlogPageCommand { get; set; }
    }
}
