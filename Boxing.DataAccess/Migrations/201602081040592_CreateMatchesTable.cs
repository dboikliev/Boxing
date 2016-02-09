namespace Boxing.Core.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateMatchesTable : DbMigration
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
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Matches");
        }
    }
}
