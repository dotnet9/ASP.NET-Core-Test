using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RESTfullDemo.Models
{
    public class ResourceCollection<T> : Resource
        where T : Resource
    {
        public ResourceCollection(List<T> items)
        {
            Items = items;
        }

        public List<T> Items { get; }
    }
}
