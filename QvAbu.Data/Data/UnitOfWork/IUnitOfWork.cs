using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace QvAbu.Data.Data.UnitOfWork
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
