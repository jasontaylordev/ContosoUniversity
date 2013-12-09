namespace ContosoUniversity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MaxLengthOnNames : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Students", "LastName", c => c.String(maxLength: 50));
            AlterColumn("dbo.Students", "FirstMidName", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Students", "FirstMidName", c => c.String());
            AlterColumn("dbo.Students", "LastName", c => c.String());
        }
    }
}
