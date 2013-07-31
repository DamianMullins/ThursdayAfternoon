using System.Data.Entity.ModelConfiguration;
using ThursdayAfternoon.Models;

namespace ThursdayAfternoon.Infrastructure.Data.Configuration
{
    public class PresentationConfiguration : EntityTypeConfiguration<Presentation>
    {
        public PresentationConfiguration()
        {
            // Table
            ToTable("Presentations");

            // Primary Key
            HasKey(c => c.Id);

            // Properties
            HasMany(p => p.Slides)
                .WithRequired(p => p.Presentation)
                .HasForeignKey(p => p.PresentationId)
                .WillCascadeOnDelete(false);
        }
    }
}