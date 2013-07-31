namespace ThursdayAfternoon.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Presentation_Add_Description : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Presentations", "Description", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Presentations", "Description");
        }
    }
}
