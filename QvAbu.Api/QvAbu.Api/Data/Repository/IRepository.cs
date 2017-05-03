using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QvAbu.Api.Models;

namespace QvAbu.Api.Data.Repository
{
    internal interface IRepository<T> where T : Entity
    {
        Task<IEnumerable<T>> GetAllAsync();
    }

    internal abstract class Repository<TEntity, TContext> : IRepository<TEntity>
        where TEntity : Entity
        where TContext : DbContext
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
