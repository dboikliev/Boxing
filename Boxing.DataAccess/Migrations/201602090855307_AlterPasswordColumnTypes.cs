namespace Boxing.Core.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlterPasswordColumnTypes : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Users", "PasswordHash");
            DropColumn("dbo.Users", "PasswordSalt");
            AddColumn("dbo.Users", "PasswordHash", c => c.Binary(nullable: false, maxLength: 1024));
            AddColumn("dbo.Users", "PasswordSalt", c => c.Binary(nullable: false, maxLength: 1024));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "PasswordHash");
            DropColumn("dbo.Users", "PasswordSalt");
            AddColumn("dbo.Users", "PasswordHash", c => c.String(nullable: false));
            AddColumn("dbo.Users", "PasswordSalt", c => c.String(nullable: false));
        }
    }
}
