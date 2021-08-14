using HH.Data.Abstractions;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using HH.Core.Domain;
using Microsoft.EntityFrameworkCore;

namespace HH.Data.SqlServer
{
    public class Repository<TContext> : IRepository
        where TContext : Microsoft.EntityFrameworkCore.DbContext
    {
        private readonly DbContext dbContext;

        public Repository(DbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IQueryable<T> FindAll<T>()
            where T : class, IEntity
        {
            return dbContext.Set<T>();
        }

        public IQueryable<T> FindByCondition<T>(
            Expression<Func<T, bool>> expression,
            Expression<Func<T, object>> include = default)
            where T : class, IEntity
        {
            var query = dbContext.Set<T>().Where(expression);

            if (include != null)
            {
                query = query.Include(include);
            }

            return query;
        }

        public async Task<T> SingleOrDefaultAsync<T>(
            Expression<Func<T, bool>> expression,
            Expression<Func<T, object>> include = default,
            CancellationToken token = default)
            where T : class, IEntity
        {
            return await FindByCondition(expression, include).SingleOrDefaultAsync(token);
        }

        public async Task Create<T>(T entity, CancellationToken token = default)
            where T : class, IEntity
        {
            await dbContext.AddAsync(entity, token);
            
            await dbContext.SaveChangesAsync(token);
        }

        public async Task Update<T>(T entity, CancellationToken token = default)
            where T : class, IEntity
        {
            dbContext.Update(entity);

            await dbContext.SaveChangesAsync(token);
        }

        public async Task DeleteAsync<T>(T entity, CancellationToken token = default)
            where T : class, IEntity
        {
            dbContext.Remove(entity);

            await dbContext.SaveChangesAsync(token);
        }
    }
}