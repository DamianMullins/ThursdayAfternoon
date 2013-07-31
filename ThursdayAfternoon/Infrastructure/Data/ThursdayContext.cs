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
            //this.Configuration.ProxyCreationEnabled = false;
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
    }
}
