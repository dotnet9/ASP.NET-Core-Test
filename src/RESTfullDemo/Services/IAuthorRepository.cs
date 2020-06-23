using RESTfullDemo.Entities;
using RESTfullDemo.Helpers;
using System;
using System.Threading.Tasks;

namespace RESTfullDemo.Services
{
    public interface IAuthorRepository :
        IRepositoryBase<Author>,
        IRepositoryBase2<Author, Guid>
    {
        Task<PagedList<Author>> GetAllAsync(AuthorResourceParameters parameters);
    }
}
