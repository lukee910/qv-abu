using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace QvAbu.Data.Data
{
    public interface IDbContext
    {
        DbSet<TEntity> Set<TEntity>() where TEntity : class;

        Task<int> SaveChangesAsync();
    }
}
