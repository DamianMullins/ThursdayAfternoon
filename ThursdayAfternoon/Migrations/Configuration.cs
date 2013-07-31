using System.Data.Entity.Migrations;
using ThursdayAfternoon.Infrastructure.Data;

namespace ThursdayAfternoon.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<ThursdayContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ThursdayContext context)
        {
            //  This method will be called after migrating to the latest version.

            //context.Users.AddOrUpdate(p => p.UserName, new User {UserName = "dmullins", Claims = new[] {"Admin"}});
        }
    }
}
