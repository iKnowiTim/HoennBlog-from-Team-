using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogMVVM.Dto
{
    public class CreatePostDto
    {
        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }
        [JsonProperty(PropertyName = "content")]
        public string Content { get; set; }
        [JsonProperty(PropertyName = "blog")]
        public string Blog { get; set; }
    }
}
