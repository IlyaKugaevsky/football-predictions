namespace Predictions.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class renameNewTour : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.NewTour", newName: "Tour");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.Tour", newName: "NewTour");
        }
    }
}
