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
            Property(u => u.FullName).HasMaxLength(200).IsRequired();
            Property(u => u.PasswordHash).HasMaxLength(1024).IsRequired();
            Property(u => u.PasswordSalt).HasMaxLength(1024).IsRequired();
            HasOptional(u => u.Login).WithRequired(login => login.User); 
        }
    }
}
