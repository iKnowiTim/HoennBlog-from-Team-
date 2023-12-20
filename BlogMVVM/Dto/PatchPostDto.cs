using BlogMVVM.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace BlogMVVM.Dto
{
    public class PatchPostDto
    {
        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }
        [JsonProperty(PropertyName = "content")]
        public string Content { get; set; }
        [JsonProperty(PropertyName = "author")]
        public int UserId { get; set; }
        [JsonProperty(PropertyName = "published")]
        public bool Published { get; set; }
    }
}
