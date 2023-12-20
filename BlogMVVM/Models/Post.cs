using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogMVVM.Models
{
    public class Post
    {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }

        [JsonProperty(PropertyName = "blog")]
        public Blog Blog { get; set; }

        [JsonProperty(PropertyName = "published")]
        public string Published { get; set; }

        [JsonProperty(PropertyName = "content")]
        public string Content { get; set; }
    }
}
