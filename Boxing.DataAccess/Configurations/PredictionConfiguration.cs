using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Boxing.Core.DataAccess.Entities;

namespace Boxing.Core.DataAccess.Configurations
{
    public class PredictionConfiguration : EntityTypeConfiguration<Prediction>
    {
        public PredictionConfiguration()
        {
            ToTable("Predictions");
            Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(p => p.PredictedWinner).IsRequired();
            HasRequired(p => p.User).WithMany(u => u.Predictions).HasForeignKey(p => p.UserId);
            HasRequired(p => p.Match);
        }
    }
}
