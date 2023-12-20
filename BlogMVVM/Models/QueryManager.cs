using BlogMVVM.Dto;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using BlogMVVM.Exceptions;

namespace BlogMVVM.Models
{
    public static class QueryManager
    {
        private static readonly string header = "application/json";        
        private static readonly string host = "https://blog.hoenn.club/api/";
        private static HttpClient httpClient = new HttpClient()
        {
            BaseAddress = new Uri(host)            
        };

        internal static async Task<List<User>> GetUsers()
        {
            string response = await httpClient.GetStringAsync("users");
            List<User> users = JsonConvert.DeserializeObject<List<User>>(response);
            return users;
        }

        internal static async Task<User> GetUser()
        {
            var response = await httpClient.GetStringAsync($"users/{Secret.CurrentUser}");

            return JsonConvert.DeserializeObject<User>(response);          
        }

        internal static async Task<List<Post>> GetPosts()
        {
            string response = await httpClient.GetStringAsync("posts");            
            List<Post> posts = JsonConvert.DeserializeObject<List<Post>>(response);
            return posts;
        }        

        internal static async Task<Post> GetPost(int blogId)
        {
            string response = await httpClient.GetStringAsync($"posts/{blogId}");
            Post blog = JsonConvert.DeserializeObject<Post>(response);
            return blog;
        }

        internal static async Task<List<Blog>> GetBlogs()
        {
            string responce = await httpClient.GetStringAsync("blogs");
            List<Blog> blogs = JsonConvert.DeserializeObject<List<Blog>>(responce);
            return blogs;
        }            

        internal static async Task CreatePost(string content, string title, string blog)
        {
            CreatePostDto createPostDto = new CreatePostDto()
            {
                Content = content,
                Title = title,
                Blog = blog
            };
            var json = JsonConvert.SerializeObject(createPostDto);
            var con = new StringContent(json, Encoding.UTF8, header);
            var result = await httpClient.PostAsync("posts/", con);
            Console.WriteLine(result.StatusCode);            
        }

        internal static async Task DeleteBlog(Blog blog)
        {
            var response = await httpClient.DeleteAsync($"blogs/{blog.Name}");
        }

        internal static async Task RegisterUser(string username, string email, string password)
        {
            if (username == null || email == null || password == null)
            {
                throw new ArgumentNullException();
            }

            CreateUserDto createUserDto = new CreateUserDto()
            {
                UserName = username,
                Email = email,
                Password = password
            };       
            var json = JsonConvert.SerializeObject(createUserDto);
            var con = new StringContent(json, encoding: Encoding.UTF8, header);
            HttpResponseMessage response = null;

            response = await httpClient.PostAsync("auth/signup", con);

            if ((int)response.StatusCode >= 500) {
                // Internal backend error
                // We don't know what's wrong
                throw new Exception("Server error. Try again later.");
            }
            if ((int)response.StatusCode < 500 && (int)response.StatusCode >= 300) {
                // Client error (Backend worked, but data was incorrect. e.g. Username already exists)
                string responseJson = await response.Content.ReadAsStringAsync(); 
                ErrorDto error = JsonConvert.DeserializeObject<ErrorDto>(responseJson);
                throw new HoennApiException(error.Error, error);
            }

            // Status OK
            // User successfully registered.
            // Api Should return user object
            User user =  JsonConvert.DeserializeObject<User>(await response.Content.ReadAsStringAsync());
        }

        internal static async Task SignIn(string userName, string password)
        {
            if (userName == null || password == null)
            {
                throw new ArgumentNullException();
            }

            LoginDto loginDto = new LoginDto()
            {                
                UserName = userName,
                Password = password
            };
            
            var json = JsonConvert.SerializeObject(loginDto);
            var con = new StringContent(json, Encoding.UTF8, header);
            HttpResponseMessage response = null;
            response = await httpClient.PostAsync("auth/login", con);

            if ((int)response.StatusCode >= 500)            
                throw new Exception("Server error. Try again later.");
            if ((int)response.StatusCode < 500 && (int)response.StatusCode >= 300)
            {
                var responseJson = await response.Content.ReadAsStringAsync();
                ErrorDto errorDto = JsonConvert.DeserializeObject<ErrorDto>(responseJson);
                throw new HoennApiException(errorDto.Error, errorDto);
            }
            var result = response.Content.ReadAsStringAsync().Result;
            var token = JsonConvert.DeserializeObject<SecretConvert>(result);
            Secret.Token = token.TokenConvert;
            Secret.CurrentUser = userName;
        }

        internal static async Task DeletePost(int postId)
        {
            await httpClient.DeleteAsync($"posts/{postId}");
        }

        internal static async Task DeleteUser(int selectedUser)
        {
            await httpClient.DeleteAsync($"users/{selectedUser}");
        }

        internal static async Task PatchUser(string userName, string email, string role)
        {
            if (userName == null || email == null || role == null)
                throw new ArgumentNullException();

            var user = new PatchUserDto()
            {
                UserName = userName,
                Email = email,
                Role = role,
                Bio = "The developer of the mobile application"
            };

            var json = JsonConvert.SerializeObject(user);
            var con = new StringContent(json, Encoding.UTF8, header);
            var response = await httpClient.PatchAsync($"users/{Secret.CurrentUser}", con);
            if (((int)response.StatusCode) >= 500)
                throw new Exception("Error Server");
            if (((int)response.StatusCode) >= 300 && ((int)response.StatusCode) < 500)
            {
                throw new Exception("Что-то пошло не так");
            }
            
        }

        internal static async Task PatchPost(string title, string content, bool published, int postId)
        {
            var blog = new PatchPostDto()
            {
                Content = content,
                Title = title,
                Published = published
            };
            var json = JsonConvert.SerializeObject(blog);
            var con = new StringContent(json, Encoding.UTF8, header);
            var result = await httpClient.PatchAsync(host + $"posts/{postId}", con);
        }        

        internal static async Task CreateBlog(string name, string displayName)
        {
            CreateBlogDto createBlogDto = new CreateBlogDto()
            {
                Name = name,
                DisplayName = displayName
            };
            var json = JsonConvert.SerializeObject(createBlogDto);
            var con = new StringContent(json, Encoding.UTF8, header);
            await httpClient.PostAsync("blogs/", con);
        }

        internal static async Task<List<Post>> GetBlogPosts(string nameBlog)
        {
            var response = await httpClient.GetStringAsync($"blogs/{nameBlog}/posts");
            return JsonConvert.DeserializeObject<List<Post>>(response);            
        }

        internal static async Task GetMeAccount(object token)
        {
            SecretConvert secretConvert = new SecretConvert()
            {
                TokenConvert = (string)token
            };

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, httpClient.BaseAddress + "auth/me");
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", (string)token);

            var response = await httpClient.SendAsync(request);

            if (((int)response.StatusCode) >= 500)
            {
                throw new Exception("ServerError");
            }
            if ((int)response.StatusCode < 500 && (int)response.StatusCode >= 300)
            {
                throw new Exception("You must re-login");
            }
            var jsonString = await response.Content.ReadAsStringAsync();
            
        }

    }
}
