namespace DAMNova.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FieldTableDataTypeAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FieldNames", "DataType", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.FieldNames", "DataType");
        }
    }
}
