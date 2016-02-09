using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Boxing.Core.DataAccess.Entities;

namespace Boxing.Core.DataAccess.Configurations
{
    public class LoginConfiguration : EntityTypeConfiguration<Login>
    {
        public LoginConfiguration()
        {
            ToTable("Logins");
            Property(login => login.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(login => login.AuthorizationToken).IsRequired();
            Property(login => login.DateCreated).IsRequired();
        }
    }
}
