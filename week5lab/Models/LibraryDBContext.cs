namespace week5lab.Models
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;

    public class LibraryDBContext : DbContext
    {
        // Your context has been configured to use a 'LibraryDBContext' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'week5lab.Models.LibraryDBContext' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'LibraryDBContext' 
        // connection string in the application configuration file.
        public LibraryDBContext()
            : base("name=LibraryDBContext")
        {
        }

        public System.Data.Entity.DbSet<week5lab.Models.Student> Students { get; set; }
        public System.Data.Entity.DbSet<week5lab.Models.Book> Books { get; set; }
        public System.Data.Entity.DbSet<week5lab.Models.CheckedOutBook> CheckedOutBooks { get; set; }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        // public virtual DbSet<MyEntity> MyEntities { get; set; }
    }
    public class Student
    {
        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public string StudentAddress { get; set; }
        public DateTime DOB { get; set; }

        public virtual ICollection<CheckedOutBook> Checkouts { get; set; }
    }
    public class Book
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public DateTime DateOfPublication { get; set; }

        public virtual ICollection<CheckedOutBook> Checkouts { get; set; }
    }
    public class CheckedOutBook
    {
        public int Id { get; set; }
        public Book Book { get; set; }
        public Student Student { get; set; }
        public DateTime CheckOutDate { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime? ReturnDate { get; set; }
    }
    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}