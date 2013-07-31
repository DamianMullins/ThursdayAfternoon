using System.Data.Entity.ModelConfiguration;
using ThursdayAfternoon.Models;

namespace ThursdayAfternoon.Infrastructure.Data.Configuration
{
    public class UserConfiguration : EntityTypeConfiguration<User>
    {
        public UserConfiguration()
        {
            // Table
            ToTable("Users");

            // Primary Key
            HasKey(c => c.Id);

            // Properties
            HasMany(c => c.Roles)
                .WithMany()
                .Map(m => m.ToTable("User_UserRole"));
            
            HasMany(p => p.Presentations)
                .WithRequired(p => p.Owner)
                .HasForeignKey(p => p.OwnerId)
                .WillCascadeOnDelete(false);
        }
    }
}