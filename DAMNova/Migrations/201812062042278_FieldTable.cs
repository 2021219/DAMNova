namespace DAMNova.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FieldTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FieldNames",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Content = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.Files", "FileType", c => c.Int(nullable: false));
            AddColumn("dbo.File_Types", "Fields", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.File_Types", "Fields");
            DropColumn("dbo.Files", "FileType");
            DropTable("dbo.FieldNames");
        }
    }
}
