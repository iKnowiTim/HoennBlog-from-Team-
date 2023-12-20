using Newtonsoft.Json;

namespace BlogMVVM.Models
{
    public class Blog
    {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "displayName")]
        public string DisplayName { get; set; }

        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }
    }
}