using DataAbstraction;
using Entities;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DataSqlServer
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected RepositoryContext repositoryContext;
        public RepositoryBase(RepositoryContext repositoryContext)
        {
            this.repositoryContext = repositoryContext;
        }

        public IQueryable<T> FindAll() =>
            repositoryContext.Set<T>();

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression) =>
            repositoryContext.Set<T>()
            .Where(expression);

        public void Create(T entity) =>
            repositoryContext.Set<T>().Add(entity);

        public void Delete(T entity) =>
            repositoryContext.Set<T>().Remove(entity);

        public void Update(T entity) =>
            repositoryContext.Set<T>().Update(entity);
    }
}
