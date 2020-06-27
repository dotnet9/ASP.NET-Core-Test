﻿using System;

namespace RESTfullDemo.Models
{
    public class BookDto: Resource
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Pages { get; set; }
        public Guid AuthorId { get; set; }
    }
}
