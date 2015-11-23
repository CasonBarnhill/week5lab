namespace week5lab.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BookDates : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CheckedOutBooks", "DueDate", c => c.DateTime());
            DropColumn("dbo.CheckedOutBooks", "BorrowDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CheckedOutBooks", "BorrowDate", c => c.DateTime());
            DropColumn("dbo.CheckedOutBooks", "DueDate");
        }
    }
}
