namespace Boxing.Core.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPredictions : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Predictions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PredictedWinner = c.String(nullable: false),
                        MatchId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Matches", t => t.MatchId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.MatchId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Predictions", "UserId", "dbo.Users");
            DropForeignKey("dbo.Predictions", "MatchId", "dbo.Matches");
            DropIndex("dbo.Predictions", new[] { "UserId" });
            DropIndex("dbo.Predictions", new[] { "MatchId" });
            DropTable("dbo.Predictions");
        }
    }
}
