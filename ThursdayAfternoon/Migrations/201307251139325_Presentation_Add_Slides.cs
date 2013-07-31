namespace ThursdayAfternoon.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Presentation_Add_Slides : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Slides", "PresentationId", "dbo.Presentations");
            DropIndex("dbo.Slides", new[] { "PresentationId" });
            CreateIndex("dbo.Slides", "PresentationId");
            AddForeignKey("dbo.Slides", "PresentationId", "dbo.Presentations", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Slides", "PresentationId", "dbo.Presentations");
            DropIndex("dbo.Slides", new[] { "PresentationId" });
            CreateIndex("dbo.Slides", "PresentationId");
            AddForeignKey("dbo.Slides", "PresentationId", "dbo.Presentations", "Id", cascadeDelete: true);
        }
    }
}
