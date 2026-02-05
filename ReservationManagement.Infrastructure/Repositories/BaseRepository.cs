using Microsoft.EntityFrameworkCore;
using ReservationManagement.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using UserSystem.Infrastructure;

namespace ReservationManagement.Infrastructure.Repositories
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class 
    {
        protected DbContext DbContext;
        protected DbSet<TEntity> DbSet;

        public BaseRepository(ReservationDbContext context) 
        {
            DbSet = context.Set<TEntity>();
            DbContext = context;
        }

        public async Task<TEntity> Add(TEntity entity) 
        {
            var result = await DbSet.AddAsync(entity);
            return result.Entity;
        }

        public void Remove(TEntity entity)
        {
            DbContext.Set<TEntity>().Remove(entity);
        }

        public virtual async Task<TEntity> GetAsync(Guid id)
        {
            return await DbSet.FindAsync(id);
        }

        public virtual async Task<IEnumerable<TEntity>> GetAsync()
        {
            return await DbSet.ToListAsync();
        }

        public async Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await DbSet.SingleOrDefaultAsync(predicate);
        }

        public async Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await DbSet.Where(predicate).ToListAsync();
        }

        public async Task<int> GetTotalPage()
        {
            return await DbSet.CountAsync();
        }

        protected IQueryable<TEntity> GetOnly()
        {
            return DbSet.AsNoTracking();
        }

        public async Task<TEntity> GetAsyncInclude(Expression<Func<TEntity, bool>> expression, string includeProperties = "")
        {
            IQueryable<TEntity> query = DbSet;
            query = query.Where(expression);

            foreach (var includeProperty in includeProperties.Trim().Split
            (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }
            return await query.FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAsyncIncludes(Expression<Func<TEntity, bool>> expression, string includeProperties = "")
        {
            IQueryable<TEntity> query = DbSet;
            query = query.Where(expression);

            foreach (var includeProperty in includeProperties.Trim().Split
            (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }
            return await query.ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAsyncInclude(string includeProperties = "")
        {
            IQueryable<TEntity> query = DbSet;

            foreach (var includeProperty in includeProperties.Trim().Split
            (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }
            return await query.ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAsyncInclude(Expression<Func<TEntity, bool>> expression, int pageSize, int pageIndex, string includeProperties = "")
        {
            IQueryable<TEntity> query = DbSet;
            query = query.Where(expression);

            foreach (var includeProperty in includeProperties.Trim().Split
            (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }
            return await query.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAsyncInclude(IQueryable<TEntity> query, int pageSize, int pageIndex, string includeProperties = "")
        {
            try
            {
                foreach (var includeProperty in includeProperties.Trim().Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }

                var rsl = await query.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
                return rsl;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }


    }
}
