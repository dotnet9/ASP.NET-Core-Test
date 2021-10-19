using System;
using Microsoft.EntityFrameworkCore;

namespace Blog.Model
{
    public class BlogContext:DbContext
    {
        public DbSet<PostInfo> PostInfos { get; set; }
        public DbSet<CategoryInfo> CategoryInfos { get; set; }
        public DbSet<UserInfo> UserInfos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlite("Data Source=blog.db");
        }
    }
}

