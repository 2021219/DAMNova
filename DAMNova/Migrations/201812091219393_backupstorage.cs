namespace DAMNova.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class backupstorage : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Files", "Locked", c => c.Boolean(nullable: false));
            AddColumn("dbo.Files", "LiterallyFile", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Files", "LiterallyFile");
            DropColumn("dbo.Files", "Locked");
        }
    }
}
