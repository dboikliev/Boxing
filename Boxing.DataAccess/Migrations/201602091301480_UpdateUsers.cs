namespace Boxing.Core.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateUsers : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "Username", c => c.String());
            AddColumn("dbo.Users", "FullName", c => c.String(nullable: false, maxLength: 200));
            AddColumn("dbo.Users", "Rating", c => c.Double(nullable: false));
            AddColumn("dbo.Matches", "Winner", c => c.Int());
            DropColumn("dbo.Users", "FirstName");
            DropColumn("dbo.Users", "LastName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "LastName", c => c.String(nullable: false, maxLength: 100));
            AddColumn("dbo.Users", "FirstName", c => c.String(nullable: false, maxLength: 100));
            DropColumn("dbo.Matches", "Winner");
            DropColumn("dbo.Users", "Rating");
            DropColumn("dbo.Users", "FullName");
            DropColumn("dbo.Users", "Username");
        }
    }
}
