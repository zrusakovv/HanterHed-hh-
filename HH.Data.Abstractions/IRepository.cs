using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using HH.Core.Domain;

namespace HH.Data.Abstractions
{
    public interface IRepository
    {
        IQueryable<T> FindAll<T>() 
            where T : class, IEntity;

        IQueryable<T> FindByCondition<T>(Expression<Func<T, bool>> expression, Expression<Func<T, object>> include)
            where T : class, IEntity;

        Task<T> SingleOrDefaultAsync<T>(
            Expression<Func<T, bool>> expression,
            Expression<Func<T, object>> include = default,
            CancellationToken token = default)
            where T : class, IEntity;
        
        Task Create<T>(T entity, CancellationToken token = default)
            where T : class, IEntity;

        Task Update<T>(T entity, CancellationToken token = default)
            where T : class, IEntity;

        Task DeleteAsync<T>(T entity, CancellationToken token = default)
            where T : class, IEntity;
    }
}