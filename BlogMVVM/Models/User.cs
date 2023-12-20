using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogMVVM.Models
{
    public class User
    {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "username")]
        public string UserName { get; set; }

        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }

        [JsonProperty(PropertyName = "role")]
        public string Role { get; set; }
    }
}
