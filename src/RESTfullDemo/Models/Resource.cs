using Newtonsoft.Json;
using System.Collections.Generic;

namespace RESTfullDemo.Models
{
    public abstract class Resource
    {
        [JsonProperty("_links", Order = 100)]
        public List<Link> Links { get; } = new List<Link>();
    }
}
