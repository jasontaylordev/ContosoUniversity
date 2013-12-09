namespace ContosoUniversity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ColumnFirstName : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Students", name: "FirstMidName", newName: "FirstName");
        }
        
        public override void Down()
        {
            RenameColumn(table: "dbo.Students", name: "FirstName", newName: "FirstMidName");
        }
    }
}
