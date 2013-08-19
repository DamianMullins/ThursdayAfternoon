using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using ThursdayAfternoon.Models;

namespace ThursdayAfternoon.Infrastructure.Data
{
    public class ThursdayContext : DbContext, IDbContext
    {
        public ThursdayContext()
            : base("ThursdayAfternoon")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.AddConfigurations();
            base.OnModelCreating(modelBuilder);
        }

        public new IDbSet<TEntity> Set<TEntity>() where TEntity : BaseEntity
        {
            return base.Set<TEntity>();
        }

        public new DbEntityEntry Entry(object entity)
        {
            return base.Entry(entity);
        }

        public int ExecuteSqlCommand(string sql, int? timeout = null, params object[] parameters)
        {
            int? previousTimeout = null;
            if (timeout.HasValue)
            {
                //store previous timeout
                previousTimeout = ((IObjectContextAdapter)this).ObjectContext.CommandTimeout;
                ((IObjectContextAdapter)this).ObjectContext.CommandTimeout = timeout;
            }

            int result = Database.ExecuteSqlCommand(sql, parameters);

            if (timeout.HasValue)
            {
                //Set previous timeout back
                ((IObjectContextAdapter)this).ObjectContext.CommandTimeout = previousTimeout;
            }
            return result;
        }
    }
}
