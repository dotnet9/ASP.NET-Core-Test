using System;
using System.Linq.Expressions;

namespace Blog.IRepository
{
    public interface IBaseRepository<TEntity> where TEntity: class, new()
    {
        Task<bool> CreateAsync(TEntity entity);
        Task<bool> DeleteAsync(int id);
        Task<bool> EditAsync(TEntity entity);
        Task<TEntity> FindAsync(int id);
        Task<bool> IsExistAsync(int id);
        Task<IEnumerable<TEntity>> QueryAsync();
        Task<IEnumerable<TEntity>> QueryAsync(Expression<Func<TEntity, bool>> func);
        Task<IEnumerable<TEntity>> QueryAsync(int page, int size, Expression<Func<TEntity, bool>> func);
        Task<bool> SaveAsync();
    }
}

