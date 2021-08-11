using HH.Data.Abstractions;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;

namespace HH.Data.SqlServer
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected DbContext dbContext;
        public RepositoryBase(DbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IQueryable<T> FindAll(CancellationToken token = default) =>
            dbContext.Set<T>();

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, CancellationToken token = default) =>
            dbContext.Set<T>()
            .Where(expression);

        public void Create(T entity, CancellationToken token = default) =>
            dbContext.Set<T>().Add(entity);

        public void Delete(T entity, CancellationToken token = default) =>
            dbContext.Set<T>().Remove(entity);

        public void Update(T entity, CancellationToken token = default) =>
            dbContext.Set<T>().Update(entity);
    }
}
