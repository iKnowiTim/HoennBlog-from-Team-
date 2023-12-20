using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogMVVM.Dto
{
    public class CreateBlogDto
    {
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
        [JsonProperty(PropertyName = "displayName")]
        public string DisplayName { get; set; }
    }
}
