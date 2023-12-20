using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogMVVM.Models
{
    public static class Secret
    {        
        public static string Token { get; set; }
        public static string CurrentUser { get; set; }
    }
}
