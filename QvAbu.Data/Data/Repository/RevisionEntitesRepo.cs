using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QvAbu.Data.Models;

namespace QvAbu.Data.Data.Repository
{
    public interface IRevisionEntitesRepo
    {
        Task<IEnumerable<object>> GetListAsync(IEnumerable<(Guid id, int revision)> keys);
    }

    public interface IRevisionEntitesRepo<in T> : IRevisionEntitesRepo where T : RevisionEntity
    {
        void Add(T entity);
    }

    public abstract class RevisionEntitesRepo<T, TContext> : Repository<T, TContext>, IRevisionEntitesRepo<T> 
        where T : RevisionEntity 
        where TContext : IDbContext
    {
        protected Func<IQueryable<T>, IQueryable<T>> IncludesFunc = queryable => queryable;

        protected RevisionEntitesRepo(TContext context) : base(context)
        {
        }

        public virtual void Add(T entity)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<object>> GetListAsync(IEnumerable<(Guid id, int revision)> keys)
        {
            // Fucking multiple enumeration
            keys = keys.ToList();

            var ids = keys.Select(_ => _.id);
            var combinedKeys = keys.Select(key => key.id.CombineRevision(key.revision)).ToList();

            return (await this.IncludesFunc(this.Context.Set<T>())
                .Where(_ => ids.Contains(_.ID))
                .ToListAsync())
                .Where(_ => combinedKeys.Contains(_.ID.CombineRevision(_.Revision)));
        }
    }
}
