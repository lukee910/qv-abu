using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace QvAbu.Api.Data.UnitOfWork
{
    public interface IUnitOfWork
    {
        Task<int> Complete();
    }

    public abstract class UnitOfWork : IUnitOfWork
    {
        protected DbContext Context;

        protected UnitOfWork(DbContext context)
        {
            this.Context = context;
        }

        public async Task<int> Complete()
        {
            return await this.Context.SaveChangesAsync();
        }
    }
}
