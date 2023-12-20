using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogMVVM.Models
{
    public class SecretConvert
    {
        [JsonProperty(PropertyName = "access_token")]
        public string TokenConvert { get; set; }
    }
}
