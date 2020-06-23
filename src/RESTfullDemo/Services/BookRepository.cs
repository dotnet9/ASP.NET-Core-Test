using Microsoft.EntityFrameworkCore;
using RESTfullDemo.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RESTfullDemo.Services
{
    public class BookRepository :
        RepositoryBase<Book, Guid>,
        IBookRepository
    {
        public BookRepository(DbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<Book>> GetBooksAsync(Guid authorId)
        {
            return await Task.FromResult(DbContext.Set<Book>().Where(book => book.AuthorId == authorId).AsEnumerable());
        }

        public async Task<Book> GetBookAsync(Guid authorId, Guid bookId)
        {
            return await DbContext.Set<Book>().SingleOrDefaultAsync(book =>
            book.AuthorId == authorId && book.Id == bookId);
        }
    }
}
