using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Boxing.Core.DataAccess.Entities;

namespace Boxing.Core.DataAccess.Configurations
{
    public class MatchConfiguration : EntityTypeConfiguration<Match>
    {
        public MatchConfiguration()
        {
            ToTable("Matches");
            Property(m => m.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(m => m.Boxer1).HasMaxLength(50).IsRequired();
            Property(m => m.Boxer2).HasMaxLength(50).IsRequired();
            Property(m => m.DateOfMatch).IsRequired();
            Property(m => m.Description).HasMaxLength(500).IsRequired();
            Property(m => m.Place).HasMaxLength(100).IsRequired();
            Property(m => m.Winner).IsOptional();
            HasRequired(m => m.Status);
        }
    }
}
