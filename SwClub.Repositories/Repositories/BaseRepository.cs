namespace SwClub.Repositories.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Threading.Tasks;
    using SwClub.Repositories.Interfaces;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Query;

    public class BaseRepository<T> : IRepository<T>
        where T : class
    {
        public BaseRepository(DbContext context)
        {
            this.DbContext = context ?? throw new ArgumentException(nameof(context));
            this.DbSet = this.DbContext.Set<T>();
        }

        public DbSet<T> DbSet { get; }

        public DbContext DbContext { get; }

        public async Task Add(T entity)
        {
            await this.DbSet.AddAsync(entity);
        }

        public async Task Add(params T[] entities)
        {
            await this.DbSet.AddRangeAsync(entities);
        }

        public async Task Add(IEnumerable<T> entities)
        {
            await this.DbSet.AddRangeAsync(entities);
        }

        public async Task Update(T entity)
        {
            await Task.Run(() => this.DbSet.Update(entity));
        }

        public async Task Update(params T[] entities)
        {
            await Task.Run(() => this.DbSet.UpdateRange(entities));
        }

        public async Task Update(IEnumerable<T> entities)
        {
            await Task.Run(() => this.DbSet.UpdateRange(entities));
        }

        public async Task Delete(object id)
        {
            var entity = await this.DbSet.FindAsync(id);
            if (entity != null)
            {
                this.DbSet.Remove(entity);
            }
        }

        public async Task Delete(T entity)
        {
            var typeInfo = typeof(T).GetTypeInfo();
            var key = this.DbContext.Model.FindEntityType(typeInfo).FindPrimaryKey().Properties.FirstOrDefault();
            var id = entity.GetType().GetProperty(key?.Name).GetValue(entity);
            if (id == null)
            {
                return;
            }

            await this.Delete(id);
        }

        public async Task Delete(params T[] entities)
        {
            await Task.Run(() => this.DbSet.RemoveRange(entities));
        }

        public async Task Delete(IEnumerable<T> entities)
        {
            await Task.Run(() => this.DbSet.RemoveRange(entities));
        }

        public async Task<T> FindById(Guid id)
        {
            return await this.DbSet.FindAsync(id);
        }

        public async Task<T> Search(params object[] keyValues)
        {
            return await Task.Run(() => this.DbSet.Find(keyValues));
        }

        public async Task<T> Single(
            Expression<Func<T, bool>> predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
            bool disableTracking = true)
        {
            IQueryable<T> query = this.DbSet;
            if (disableTracking)
            {
                query = query.AsNoTracking();
            }

            if (include != null)
            {
                query = include(query);
            }

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            if (orderBy != null)
            {
                return await orderBy(query).FirstOrDefaultAsync();
            }

            return await query.FirstOrDefaultAsync();
        }

        public async Task<IQueryable<T>> QueryAll(bool disableTracking = true)
        {
            return await Task.Run(() => disableTracking ? this.DbSet.AsNoTracking() : this.DbSet);
        }

        public async Task<IQueryable<T>> QueryCondition(
            Expression<Func<T, bool>> expression,
            bool disableTracking = true)
        {
            return await Task.Run(() => disableTracking ? this.DbSet.Where(expression).AsNoTracking() : this.DbSet.Where(expression));
        }

        public async Task<bool> Any(Expression<Func<T, bool>> expression)
        {
            return await Task.Run(() => this.DbSet.Any(expression));
        }

        public async Task<IQueryable<TType>> Select<TType>(Expression<Func<T, TType>> select)
        {
            return await Task.Run(() => this.DbSet.Select(select));
        }

        public void Dispose()
        {
            this.DbContext?.Dispose();
        }
    }
}
