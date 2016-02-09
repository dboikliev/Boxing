namespace Boxing.Core.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAuthorizationTokenToUsers : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "AuthorizationToken", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "AuthorizationToken");
        }
    }
}
