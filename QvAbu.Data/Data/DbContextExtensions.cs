using Microsoft.EntityFrameworkCore;
using QvAbu.Data.Models;

namespace QvAbu.Data.Data
{
    public static class DbContextExtensions
    {
        public static void SetRevisionEntityKey<T>(this ModelBuilder modelBuilder) where T : RevisionEntity
        {
            modelBuilder.Entity<T>()
                .HasKey(_ => new { _.ID, _.Revision });
        }
    }
}
