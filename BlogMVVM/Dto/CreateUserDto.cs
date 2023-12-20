using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogMVVM.Dto
{
    class CreateUserDto
    {
        [JsonProperty(PropertyName = "username")]
        public string UserName { get; set; }
        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }
        [JsonProperty(PropertyName = "password")]
        public string Password { get; set; }
    }
}
