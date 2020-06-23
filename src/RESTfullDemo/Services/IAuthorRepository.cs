using RESTfullDemo.Entities;
using RESTfullDemo.Models;
using System;
using System.Collections.Generic;

namespace RESTfullDemo.Services
{
    public interface IAuthorRepository :
        IRepositoryBase<Author>,
        IRepositoryBase2<Author, Guid>
    {
    }
}
