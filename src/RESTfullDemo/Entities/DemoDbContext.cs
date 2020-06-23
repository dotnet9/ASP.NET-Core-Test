using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace RESTfullDemo.Entities
{
    public class DemoDbContext : DbContext
    {
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DemoDbContext(DbContextOptions<DemoDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            int authorCount = 10;
            Random rd = new Random(DateTime.Now.Millisecond);
            List<Author> lstAuthors = new List<Author>();
            for (int i = 0; i < authorCount; i++)
            {
                var author = new Author
                {
                    Id = Guid.NewGuid(),
                    Name = $"Author {i}",
                    BirthDate=new DateTimeOffset(DateTime.Now.AddYears(-30)),
                    Email = $"author{i}@xxx.com"
                };
                lstAuthors.Add(author);
            }
            modelBuilder.Entity<Author>().HasData(lstAuthors);
        }
    }
}
