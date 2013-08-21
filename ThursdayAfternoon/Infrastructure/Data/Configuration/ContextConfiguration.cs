using System.Data.Entity.Migrations;
using ThursdayAfternoon.Infrastructure.Services.Install;

namespace ThursdayAfternoon.Infrastructure.Data.Configuration
{
    public class ContextConfiguration : DbMigrationsConfiguration<ThursdayContext>
    {
        public ContextConfiguration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(ThursdayContext context)
        {
            if (!context.Database.Exists())
            {
                new InstallService(context).InstallData();
            }
        }
    }
}