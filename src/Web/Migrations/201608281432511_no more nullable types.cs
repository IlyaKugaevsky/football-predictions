namespace Predictions.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class nomorenullabletypes : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Prediction", "Sum", c => c.Int(nullable: false));
            AlterColumn("dbo.Prediction", "Score", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Prediction", "Difference", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Prediction", "Outcome", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Prediction", "Outcome", c => c.Boolean());
            AlterColumn("dbo.Prediction", "Difference", c => c.Boolean());
            AlterColumn("dbo.Prediction", "Score", c => c.Boolean());
            AlterColumn("dbo.Prediction", "Sum", c => c.Int());
        }
    }
}
