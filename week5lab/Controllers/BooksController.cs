using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using week5lab.Models;

namespace week5lab.Controllers
{

    public class BooksController: ApiController
    {
        private LibraryDBContext db = new LibraryDBContext();

        [Route("api/books/Checkout")]
        public IHttpActionResult CheckOut(CheckOutInfo info)
        {
            Book book = db.Books.Find(info.BookId);
            if (book == null)
            {
                return NotFound();
            }

            Student student  = db.Students.Find(info.StudentId);
            if (student == null)
            {
                return NotFound();
            }

            CheckedOutBook cob = new CheckedOutBook();
            cob.Student = student;
            cob.Book = book;
            cob.CheckOutDate = DateTime.Now;
            cob.DueDate = cob.CheckOutDate.AddMonths(1);

            db.CheckedOutBooks.Add(cob);
            db.SaveChanges();

            var result = new { cob.CheckOutDate, cob.DueDate, cob.Book.BookId, cob.Student.StudentId };
            return Ok(result);
        }
        // GET: api/Books
        public List<Book> GetBooks()
        {
            return db.Books.ToList();
        }

        // GET: api/Books/5
        [ResponseType(typeof(Book))]
        public IHttpActionResult GetBook(int id)
        {
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return NotFound();
            }

            return Ok(book);
        }
        // PUT: api/Books/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutBook(int id, Book book)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != book.BookId)
            {
                return BadRequest();
            }

            db.Entry(book).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentsExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        private bool StudentsExists(int id)
        {
            throw new NotImplementedException();
        }

        // POST: api/Books

        [ResponseType(typeof(Book))]
        public IHttpActionResult PostStudent(Book book)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Books.Add(book);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = book.BookId }, book);
        }
        // Delete: api/Books/5
        [ResponseType(typeof(Book))]
        public IHttpActionResult DeleteBook(int id)
        {
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return NotFound();
            }

            db.Books.Remove(book);
            db.SaveChanges();

            return Ok(book);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BookExists(int id)
        {
            return db.Books.Count(e => e.BookId == id) > 0;
        }
    }
}