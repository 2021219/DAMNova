namespace DAMNova.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SetupDatabase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DateTimes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Content = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Files",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Code = c.String(),
                        Fields = c.String(),
                        AccessLevel = c.Int(nullable: false),
                        FilePath = c.String(),
                        LastModifiedOn = c.DateTime(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                        Login_UserID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Logins", t => t.Login_UserID)
                .Index(t => t.Login_UserID);
            
            CreateTable(
                "dbo.Logins",
                c => new
                    {
                        UserID = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                        Password = c.String(),
                        Active = c.Boolean(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                        Role_RoleID = c.Int(),
                    })
                .PrimaryKey(t => t.UserID)
                .ForeignKey("dbo.Roles", t => t.Role_RoleID)
                .Index(t => t.Role_RoleID);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        RoleID = c.Int(nullable: false, identity: true),
                        RoleName = c.String(),
                        AccessLevel = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.RoleID);
            
            CreateTable(
                "dbo.File_Types",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        FieldName = c.String(),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Floats",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Content = c.Single(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Ints",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Content = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Strings",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Content = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Logins", "Role_RoleID", "dbo.Roles");
            DropForeignKey("dbo.Files", "Login_UserID", "dbo.Logins");
            DropIndex("dbo.Logins", new[] { "Role_RoleID" });
            DropIndex("dbo.Files", new[] { "Login_UserID" });
            DropTable("dbo.Strings");
            DropTable("dbo.Ints");
            DropTable("dbo.Floats");
            DropTable("dbo.File_Types");
            DropTable("dbo.Roles");
            DropTable("dbo.Logins");
            DropTable("dbo.Files");
            DropTable("dbo.DateTimes");
        }
    }
}
