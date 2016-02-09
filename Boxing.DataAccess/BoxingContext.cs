using System.Data.Entity;
using Boxing.Core.DataAccess.Configurations;
using Boxing.Core.DataAccess.Entities;
using Boxing.Core.DataAccess.Migrations;

namespace Boxing.Core.DataAccess
{
    public class BoxingContext : DbContext
    {
        public BoxingContext() : base("boxingConnection")
        {
            
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Match> Matches { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Configurations.Add(new UserConfiguration());
            modelBuilder.Configurations.Add(new MatchConfiguration());
        }

        public static void SetInitializer()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<BoxingContext, Configuration>());
        }
    }
}
