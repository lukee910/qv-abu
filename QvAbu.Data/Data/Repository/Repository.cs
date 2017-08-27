using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QvAbu.Data.Models;

namespace QvAbu.Data.Data.Repository
{
    public interface IRepository<T> where T : Entity
    {
        Task<IEnumerable<T>> GetAllAsync();
    }

    public abstract class Repository<TEntity, TContext> : IRepository<TEntity>
        where TEntity : Entity
        where TContext : IDbContext
    {
        #region Members

        protected readonly TContext Context;

        #endregion

        #region Ctor

        protected Repository(TContext context)
        {
            this.Context = context;
        }

        #endregion

        #region Public Methods

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await this.Context.Set<TEntity>().ToListAsync();
        }

        #endregion
    }
}
