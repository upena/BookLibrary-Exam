using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BookLibrary.Model;
using BookLibrary.Web.Models;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.IO;

namespace BookLibrary.Web.Controllers
{
    public class BooksController : Controller
    {
        private BookLibraryEntities db = new BookLibraryEntities();

        // GET: Books
        public async Task<ActionResult> Index()
        {
            var books = db.Books.Include(b => b.Author);

            var booksList = await books.ToListAsync();
            List<BookViewModel> result = new List<BookViewModel>();
            foreach (var book in booksList)
            {
                BookViewModel b = new BookViewModel() { Id = book.BookId, Title= book.Title, AuthorId = book.AuthorId, AuthorName = book.Author.AuthorName, Price= book.Price, Editorial= book.Editorial };
                result.Add(b);
            }

            return View(result);
        }

        public async Task<ActionResult> IndexAuthor(int? AuthorId)
        {
            ViewBag.AuthorId = new SelectList(db.Authors, "AuthorId", "AuthorName");
            List<BookViewModel> result = new List<BookViewModel>();
            if (AuthorId != null)
            {
                var books = db.Books.Include(b => b.Author).Where(b => b.AuthorId == AuthorId);

                var booksList = await books.ToListAsync();

                foreach (var book in booksList)
                {
                    BookViewModel b = new BookViewModel() { Id = book.BookId, Title = book.Title, AuthorId = book.AuthorId, AuthorName = book.Author.AuthorName, Price = book.Price, Editorial = book.Editorial };
                    result.Add(b);
                }
            }
            return View(result);
        }

        private BookViewModel GetModel(Book book)
        {
            BookViewModel b = new BookViewModel() { Id = book.BookId, Title = book.Title, AuthorId = book.AuthorId, Price = book.Price, Editorial = book.Editorial };

            return b;
        }
        // GET: Books/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = await db.Books.FindAsync(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            BookViewModel model = GetModel(book);
            return View(model); 
        }

        // GET: Books/Create
        public ActionResult Create()
        {
            ViewBag.AuthorId = new SelectList(db.Authors, "AuthorId", "AuthorName");
            BookViewModel model = new BookViewModel();
            return View(model);
        }

        // POST: Books/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BookViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Book book = new Book();
                    book.Title = model.Title;
                    book.AuthorId = model.AuthorId;
                    book.Price = model.Price;
                    book.Editorial = model.Editorial;
                    db.Books.Add(book);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (DbUpdateException dbEx)
                {
                    SqlException sqlEx = dbEx.GetBaseException() as SqlException;
                    if (sqlEx.Errors[0].Number == 2601)
                    {
                        TempData["ErrorMessage"] = "Duplicated code";
                    }
                    else
                        TempData["ErrorMessage"] = sqlEx.Errors[0].Message;

                    ViewBag.AuthorId = new SelectList(db.Authors, "AuthorId", "AuthorName", model.AuthorId);

                    return View(model);
                }
            }

            ViewBag.AuthorId = new SelectList(db.Authors, "AuthorId", "AuthorName", model.AuthorId);
            return View(model);
        }

        // GET: Books/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = await db.Books.FindAsync(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            ViewBag.AuthorId = new SelectList(db.Authors, "AuthorId", "AuthorName", book.AuthorId);
            BookViewModel model = GetModel(book);
            return View(model);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(BookViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Book book = await db.Books.FindAsync(model.Id);
                    book.Title = model.Title;
                    book.AuthorId = model.AuthorId;
                    book.Price = model.Price;
                    book.Editorial = model.Editorial;
                    db.Entry(book).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                catch (DbUpdateException dbEx)
                {
                    SqlException sqlEx = dbEx.GetBaseException() as SqlException;
                    if (sqlEx.Errors[0].Number == 2601)
                    {
                        TempData["ErrorMessage"] = "Duplicated code";
                    }
                    else
                        TempData["ErrorMessage"] = sqlEx.Errors[0].Message;

                    ViewBag.AuthorId = new SelectList(db.Authors, "AuthorId", "AuthorName", model.AuthorId);

                    return View(model);
                }
            }
            ViewBag.AuthorId = new SelectList(db.Authors, "AuthorId", "AuthorName", model.AuthorId);
            return View(model);
        }

        // GET: Books/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = await db.Books.FindAsync(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            BookViewModel model = GetModel(book);
            return View(model);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            try
            {
                Book book = await db.Books.FindAsync(id);
                db.Books.Remove(book);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            catch (DbUpdateException dbEx)
            {
                SqlException sqlEx = dbEx.GetBaseException() as SqlException;
                if (sqlEx.Errors[0].Number == 547)
                {
                    TempData["ErrorMessage"] = "Record in use";
                }
                else
                    TempData["ErrorMessage"] = sqlEx.Errors[0].Message;

                return RedirectToAction("Delete", new { id = id });
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
