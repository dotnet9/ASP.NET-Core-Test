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

            int authorCount = 200;
            Random rd = new Random(DateTime.Now.Millisecond);
            List<Author> lstAuthors = new List<Author>();
            for (int i = 0; i < authorCount; i++)
            {
                var author = new Author
                {
                    Id = Guid.NewGuid(),
                    Name = $"Author {i}",
                    BirthDate = new DateTimeOffset(DateTime.Now.AddYears(-1 * rd.Next(20, 50))),
                    BirthPlace = $"place {i}",
                    Email = $"author{i}@xxx.com"
                };
                lstAuthors.Add(author);

                //int bookCount = rd.Next(3, 20);
                //for (int j = 0; j < bookCount; j++)
                //{
                //    author.Books.Add(new Book
                //    {
                //        Id = Guid.NewGuid(),
                //        Title = $"test book title {i}-{j}",
                //        Description = $"test book des {i}-{j}",
                //        Pages = rd.Next(250, 1000),
                //        AuthorId = author.Id,
                //        Author = author
                //    });
                //}
            }
            modelBuilder.Entity<Author>().HasData(lstAuthors);
        }
    }
}
