using System;
using System.ComponentModel.DataAnnotations;

namespace Blog.Model
{
    public class CategoryInfo:BaseEntity
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Content { get; set; }
        public CategoryInfo(string title, string content)
        {
            Title = title;
            Content = content;
        }
    }
}

