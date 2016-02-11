using Boxing.Core.DataAccess.Entities;

namespace Boxing.Core.DataAccess.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Boxing.Core.DataAccess.BoxingContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Boxing.Core.DataAccess.BoxingContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
            context.Statuses.Add(new Status { Name = "Active" });
            context.Statuses.Add(new Status { Name = "Finished" });
            context.Statuses.Add(new Status { Name = "Canceled" });
            context.SaveChanges();
        }
    }
}
