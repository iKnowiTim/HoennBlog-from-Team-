using BlogMVVM.Views;
using MvvmHelpers.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BlogMVVM
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AppShell : Shell
    {
        public AsyncCommand LogoutCommand { get; set; }

        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(AddPostPage), typeof(AddPostPage));
            Routing.RegisterRoute(nameof(MyBlogPage), typeof(MyBlogPage));
            Routing.RegisterRoute(nameof(RegistrationPage), typeof(RegistrationPage));
            Routing.RegisterRoute(nameof(ProgramBlogPage), typeof(ProgramBlogPage));
            Routing.RegisterRoute(nameof(AddBlogPage), typeof(AddBlogPage));
            Routing.RegisterRoute(nameof(EditProfilePage), typeof(EditProfilePage));
            LogoutCommand = new AsyncCommand(Logout);
            BindingContext = this;
        }

        private async Task Logout()
        {
            App.Current.Properties.Remove("Token");
            App.Current.Properties["UserName"] = "Guest";
            await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
        }        
    }
}