using System;
using System.Linq.Expressions;
using Blog.IRepository;
using Microsoft.EntityFrameworkCore;

namespace Blog.Repositoty
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity>
    {
        public DbContext DbContext { get; set; }
        public BaseRepository(DbContext dbContext)
        {
            DbContext = dbContext;
        }

        public async Task<bool> CreateAsync(TEntity entity)
        {
            return await DbContext.Set<TEntity>().AddAsync(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var obj = FindAsync(id);
            if (obj != null)
            {
                await DbContext.Set<TEntity>().Remove(obj);
            }
            return false;
        }

        public Task<bool> EditAsync(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task<TEntity> FindAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsExistAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> QueryAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> QueryAsync(Expression<Func<T, bool>> func)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> QueryAsync(int page, int size, Expression<Func<T, bool>> func)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SaveAsync()
        {
            throw new NotImplementedException();
        }
    }
}

