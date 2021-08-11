using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;

namespace HH.Data.Abstractions
{
    public interface IRepositoryBase<T>
    {
        IQueryable<T> FindAll(CancellationToken token = default);
        IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, CancellationToken token = default);
        void Create(T entity, CancellationToken token = default);
        void Update(T entity, CancellationToken token = default);
        void Delete(T entity, CancellationToken token = default);
    }
}
