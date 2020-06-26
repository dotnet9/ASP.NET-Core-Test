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

            modelBuilder.Seed();
        }
    }
}
