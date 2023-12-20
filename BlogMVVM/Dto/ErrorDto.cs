using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogMVVM.Dto
{
    public class ErrorDto
    {
        [JsonProperty(PropertyName = "message")]
        public readonly List<string> Messages;
        [JsonProperty(PropertyName = "error")]
        public readonly string Error;
        [JsonProperty(PropertyName = "statusCode")]
        public readonly int StatusCode;
    }
}
