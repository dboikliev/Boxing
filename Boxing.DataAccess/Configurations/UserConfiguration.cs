using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Boxing.Core.DataAccess.Entities;

namespace Boxing.Core.DataAccess.Configurations
{
    class UserConfiguration : EntityTypeConfiguration<User>
    {
        public UserConfiguration()
        {
            ToTable("Users");
            Property(u => u.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(u => u.FirstName).HasMaxLength(100).IsRequired();
            Property(u => u.LastName).HasMaxLength(100).IsRequired();
            Property(u => u.PasswordHash).HasMaxLength(1024).IsRequired();
            Property(u => u.PasswordSalt).HasMaxLength(1024).IsRequired();
            Property(u => u.AuthorizationToken).IsRequired();
        }
    }
}
