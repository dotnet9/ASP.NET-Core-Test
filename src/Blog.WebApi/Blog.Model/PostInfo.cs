using System;
using System.ComponentModel.DataAnnotations;

namespace Blog.Model
{
    public class PostInfo:BaseEntity
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Content { get; set; }
        [Required]
        public int CategoryID { get; set; }
        public CategoryInfo Category { get; set; }     
        public int ReadCount { get; set; }
        public int LikeCount { get; set; }

        public PostInfo(string title, string content, int categoryID)
        {
            Title = title;
            Content = content;
            CategoryID = categoryID;
        }
    }
}

