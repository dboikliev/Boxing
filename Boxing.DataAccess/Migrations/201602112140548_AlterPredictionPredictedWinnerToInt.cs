namespace Boxing.Core.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlterPredictionPredictedWinnerToInt : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Predictions", "PredictedWinner", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Predictions", "PredictedWinner", c => c.String(nullable: false));
        }
    }
}
