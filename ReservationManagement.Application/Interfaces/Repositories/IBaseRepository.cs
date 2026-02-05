using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace UserSystem.Infrastructure
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        Task<TEntity> Add(TEntity entity);

        void Remove(TEntity entity);

        Task<TEntity> GetAsync(Guid id);

        Task<IEnumerable<TEntity>> GetAsync();

        Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);

        Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate);

        Task<int> GetTotalPage();

        Task<TEntity> GetAsyncInclude(Expression<Func<TEntity, bool>> expression, string includeProperties = "");
        Task<IEnumerable<TEntity>> GetAsyncIncludes(Expression<Func<TEntity, bool>> expression, string includeProperties = "");

        Task<IEnumerable<TEntity>> GetAsyncInclude(string includeProperties = "");

        Task<IEnumerable<TEntity>> GetAsyncInclude(Expression<Func<TEntity, bool>> expression, int pageSize, int pageIndex, string includeProperties = "");

        Task<IEnumerable<TEntity>> GetAsyncInclude(IQueryable<TEntity> expression, int pageSize, int pageIndex, string includeProperties = "");
    }
}
