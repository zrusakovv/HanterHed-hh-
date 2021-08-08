using HH.Data.Abstractions;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace HH.Data.SqlServer
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected DbContext dbContext;
        public RepositoryBase(DbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IQueryable<T> FindAll() =>
            dbContext.Set<T>();

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression) =>
            dbContext.Set<T>()
            .Where(expression);

        public void Create(T entity) =>
            dbContext.Set<T>().Add(entity);

        public void Delete(T entity) =>
            dbContext.Set<T>().Remove(entity);

        public void Update(T entity) =>
            dbContext.Set<T>().Update(entity);
    }
}
