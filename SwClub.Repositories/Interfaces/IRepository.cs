namespace SwClub.Repositories.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore.Query;

    public interface IRepository<T> : IDisposable
        where T : class
    {
        Task Add(T entity);

        Task Add(params T[] entities);

        Task Add(IEnumerable<T> entities);

        Task Update(T entity);

        Task Update(params T[] entities);

        Task Update(IEnumerable<T> entities);

        Task Delete(object id);

        Task Delete(T entity);

        Task Delete(params T[] entities);

        Task Delete(IEnumerable<T> entities);

        Task<T> FindById(Guid id);

        Task<T> Search(params object[] keyValues);

        Task<T> Single(
            Expression<Func<T, bool>> predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
            bool disableTracking = true);

        Task<IQueryable<T>> QueryAll(bool disableTracking = true);

        Task<IQueryable<T>> QueryCondition(
            Expression<Func<T, bool>> expression,
            bool disableTracking = true);

        Task<bool> Any(Expression<Func<T, bool>> expression);

        Task<IQueryable<TType>> Select<TType>(Expression<Func<T, TType>> select);
    }
}
