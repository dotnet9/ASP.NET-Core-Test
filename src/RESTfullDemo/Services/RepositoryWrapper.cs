using RESTfullDemo.Entities;
using System;

namespace RESTfullDemo.Services
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private IAuthorRepository authorRepository = null;
        private IBookRepository bookRepository = null;
        public RepositoryWrapper(DemoDbContext dbContext)
        {
            this.DemoDbContext = dbContext;
        }
        public IBookRepository Book => bookRepository ?? new BookRepository(DemoDbContext);

        public IAuthorRepository Author => authorRepository ?? new AuthorRepository(DemoDbContext);
        public DemoDbContext DemoDbContext { get; }
    }
}
