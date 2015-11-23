namespace week5lab.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BookId : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Books",
                c => new
                    {
                        BookId = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Author = c.String(),
                        DateOfPublication = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.BookId);
            
            CreateTable(
                "dbo.CheckedOutBooks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CheckOutDate = c.DateTime(nullable: false),
                        BorrowDate = c.DateTime(),
                        ReturnDate = c.DateTime(),
                        Book_BookId = c.Int(),
                        Student_StudentId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Books", t => t.Book_BookId)
                .ForeignKey("dbo.Students", t => t.Student_StudentId)
                .Index(t => t.Book_BookId)
                .Index(t => t.Student_StudentId);
            
            CreateTable(
                "dbo.Students",
                c => new
                    {
                        StudentId = c.Int(nullable: false, identity: true),
                        StudentName = c.String(),
                        StudentAddress = c.String(),
                        DOB = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.StudentId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CheckedOutBooks", "Student_StudentId", "dbo.Students");
            DropForeignKey("dbo.CheckedOutBooks", "Book_BookId", "dbo.Books");
            DropIndex("dbo.CheckedOutBooks", new[] { "Student_StudentId" });
            DropIndex("dbo.CheckedOutBooks", new[] { "Book_BookId" });
            DropTable("dbo.Students");
            DropTable("dbo.CheckedOutBooks");
            DropTable("dbo.Books");
        }
    }
}
