namespace Boxing.Core.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateDatabase : DbMigration
    {
        public override void Up()
        {
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
                        Winner = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Username = c.String(nullable: false, maxLength: 50),
                        FullName = c.String(nullable: false, maxLength: 200),
                        PasswordHash = c.Binary(nullable: false, maxLength: 1024),
                        PasswordSalt = c.Binary(nullable: false, maxLength: 1024),
                        AuthenticationToken = c.Guid(nullable: false),
                        Rating = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Users");
            DropTable("dbo.Matches");
        }
    }
}
