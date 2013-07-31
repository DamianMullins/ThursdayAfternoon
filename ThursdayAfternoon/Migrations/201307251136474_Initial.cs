namespace ThursdayAfternoon.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Presentations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        OwnerId = c.Int(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        ModifiedOn = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.OwnerId)
                .Index(t => t.OwnerId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                        Identifier = c.Guid(nullable: false),
                        PasswordHash = c.String(),
                        PasswordSalt = c.String(),
                        Email = c.String(),
                        Active = c.Boolean(nullable: false),
                        LastLoginDate = c.DateTime(),
                        CreatedOn = c.DateTime(nullable: false),
                        ModifiedOn = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserRoles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 255),
                        Active = c.Boolean(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        ModifiedOn = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Slides",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PresentationId = c.Int(nullable: false),
                        Title = c.String(),
                        Content = c.String(),
                        CreatedOn = c.DateTime(nullable: false),
                        ModifiedOn = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Presentations", t => t.PresentationId, cascadeDelete: true)
                .Index(t => t.PresentationId);
            
            CreateTable(
                "dbo.User_UserRole",
                c => new
                    {
                        User_Id = c.Int(nullable: false),
                        UserRole_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.User_Id, t.UserRole_Id })
                .ForeignKey("dbo.Users", t => t.User_Id, cascadeDelete: true)
                .ForeignKey("dbo.UserRoles", t => t.UserRole_Id, cascadeDelete: true)
                .Index(t => t.User_Id)
                .Index(t => t.UserRole_Id);


            Sql("INSERT INTO dbo.UserRoles ( Name, Active, CreatedOn ) VALUES ( 'Admin', 1, GETDATE() ), ( 'User', 1, GETDATE() )");

            Sql("INSERT INTO dbo.Users ( UserName, Identifier, PasswordHash, PasswordSalt, Email, Active, LastLoginDate, CreatedOn, ModifiedOn ) VALUES ( 'djmelonz', '{8DB5F881-5A47-4FF2-A518-CB8B6559EE59}', '3756EDB332DF48BDCCDBA876718975657CA1A28D', 'h2afxCQ=', 'damian@lowflyingowls.co.uk', 1, null, GETDATE(), GETDATE() )");

            Sql("INSERT INTO dbo.User_UserRole ( User_Id, UserRole_Id ) VALUES ( 1, 1 ), ( 1, 2 )");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Slides", "PresentationId", "dbo.Presentations");
            DropForeignKey("dbo.Presentations", "OwnerId", "dbo.Users");
            DropForeignKey("dbo.User_UserRole", "UserRole_Id", "dbo.UserRoles");
            DropForeignKey("dbo.User_UserRole", "User_Id", "dbo.Users");
            DropIndex("dbo.Slides", new[] { "PresentationId" });
            DropIndex("dbo.Presentations", new[] { "OwnerId" });
            DropIndex("dbo.User_UserRole", new[] { "UserRole_Id" });
            DropIndex("dbo.User_UserRole", new[] { "User_Id" });
            DropTable("dbo.User_UserRole");
            DropTable("dbo.Slides");
            DropTable("dbo.UserRoles");
            DropTable("dbo.Users");
            DropTable("dbo.Presentations");
        }
    }
}
