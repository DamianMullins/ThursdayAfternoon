using System.Data.Entity.ModelConfiguration;
using ThursdayAfternoon.Models;

namespace ThursdayAfternoon.Infrastructure.Data.Configuration
{
    public class UserRoleConfiguration : EntityTypeConfiguration<UserRole>
    {
        public UserRoleConfiguration()
        {
            // Table
            ToTable("UserRoles");

            // Primary Key
            HasKey(c => c.Id);

            // Properties
            Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(255);
        }
    }
}