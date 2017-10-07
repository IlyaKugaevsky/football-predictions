namespace Predictions.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class boolIsClosedtoPredictionschema : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Prediction", "IsClosed", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Prediction", "IsClosed");
        }
    }
}
