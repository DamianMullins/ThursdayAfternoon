using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using ThursdayAfternoon.Models;

namespace ThursdayAfternoon.Infrastructure.Data
{
    public interface IDbContext
    {
        IDbSet<TEntity> Set<TEntity>() where TEntity : BaseEntity;
        DbEntityEntry Entry(object entity);
        int SaveChanges();
        int ExecuteSqlCommand(string sql, int? timeout = null, params object[] parameters);
    }
}
