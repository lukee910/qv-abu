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
        protected DbContext context;

        public UnitOfWork(DbContext context)
        {
            this.context = context;
        }

        public async Task<int> Complete()
        {
            return await this.context.SaveChangesAsync();
        }
    }
}
