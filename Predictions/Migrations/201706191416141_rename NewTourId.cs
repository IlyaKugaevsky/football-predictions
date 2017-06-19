namespace Predictions.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class renameNewTourId : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Match", name: "NewTourId", newName: "TourId");
            RenameIndex(table: "dbo.Match", name: "IX_NewTourId", newName: "IX_TourId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Match", name: "IX_TourId", newName: "IX_NewTourId");
            RenameColumn(table: "dbo.Match", name: "TourId", newName: "NewTourId");
        }
    }
}
