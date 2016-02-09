namespace Boxing.Core.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Create : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Logins",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AuthorizationToken = c.String(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 100),
                        LastName = c.String(nullable: false, maxLength: 100),
                        PasswordHash = c.Binary(nullable: false, maxLength: 1024),
                        PasswordSalt = c.Binary(nullable: false, maxLength: 1024),
                        LoginId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.LoginId, unique: true);
            
            CreateTable(
                "dbo.Matches",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Boxer1 = c.String(nullable: false, maxLength: 50),
                        Boxer2 = c.String(nullable: false, maxLength: 50),
                        Place = c.String(nullable: false, maxLength: 100),
                        DateOfMatch = c.DateTime(nullable: false),
                        Description = c.String(nullable: false, maxLength: 500),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Logins", "Id", "dbo.Users");
            DropIndex("dbo.Users", new[] { "LoginId" });
            DropIndex("dbo.Logins", new[] { "Id" });
            DropTable("dbo.Matches");
            DropTable("dbo.Users");
            DropTable("dbo.Logins");
        }
    }
}
