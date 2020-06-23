using Microsoft.EntityFrameworkCore;
using RESTfullDemo.Entities;
using System;

namespace RESTfullDemo.Services
{
    public class AuthorRepository :
        RepositoryBase<Author, Guid>,
        IAuthorRepository
    {
        public AuthorRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
