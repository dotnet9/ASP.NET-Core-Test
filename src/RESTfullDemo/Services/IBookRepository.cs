using RESTfullDemo.Entities;
using RESTfullDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RESTfullDemo.Services
{
    public interface IBookRepository :
        IRepositoryBase<Book>,
        IRepositoryBase2<Book, Guid>
    {
        Task<IEnumerable<Book>> GetBooksAsync(Guid authorId);
        Task<Book> GetBookAsync(Guid authorId, Guid bookId);
    }
}
