namespace Boxing.Core.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMatchStatuses : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Statuses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Matches", "StatusId", c => c.Int(nullable: false));
            CreateIndex("dbo.Matches", "StatusId");
            AddForeignKey("dbo.Matches", "StatusId", "dbo.Statuses", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Matches", "StatusId", "dbo.Statuses");
            DropIndex("dbo.Matches", new[] { "StatusId" });
            DropColumn("dbo.Matches", "StatusId");
            DropTable("dbo.Statuses");
        }
    }
}
