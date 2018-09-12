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
using System.IO;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;

namespace BookLibrary.Web.Controllers
{
    public class CategoriesController : Controller
    {
        private BookLibraryEntities db = new BookLibraryEntities();

        // GET: Categories
        public async Task<ActionResult> Index()
        {
            var categories = await db.Categories.ToListAsync();

            
            
            List<CategoryViewModel> result = new List<CategoryViewModel>();
            foreach(var cat in categories)
            {
                CategoryViewModel c = new CategoryViewModel() { Id = cat.CategoryId, Name = cat.CategoryName };
                result.Add(c);
            }
            return View(result);
        }
        private CategoryViewModel GetModel(Category category)
        {
            CategoryViewModel model = new CategoryViewModel();
            model.Id = category.CategoryId;
            model.Name = category.CategoryName;
            foreach (var book in category.Books)
            {
                BookViewModel b = new BookViewModel() { Id = book.BookId, Title = book.Title };
                model.Books.Add(b);
            }

            return model;
        }
        // GET: Categories/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = await db.Categories.FindAsync(id);
            if (category == null)
            {
                return HttpNotFound();
            }

            CategoryViewModel model = GetModel(category);
            BooksAdded = model.Books;
            return View(model);
        }

        private async Task<List<BookViewModel>> GetPendingBooks()
        {
            List<Book> books = await db.Books.ToListAsync();

            List<BookViewModel> booksCat = new List<BookViewModel>();
            foreach (var b in books)
            {
                if (BooksAdded != null && BooksAdded.Any(ba => ba.Id == b.BookId))
                    continue;
                BookViewModel book = new BookViewModel() { Id = b.BookId, Title = b.Title };
                booksCat.Add(book);
            }

            return booksCat;
        }
        // GET: Categories/Create
        public async Task<ActionResult> Create()
        {
            CategoryViewModel model = new CategoryViewModel();
            BooksAdded = null;
            List<BookViewModel> allBooks = await GetPendingBooks();
            
            ViewBag.BookId = new SelectList(allBooks, "Id", "Title");
            return View(model);
        }

        // POST: Categories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CategoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (BooksAdded != null)
                    model.Books = BooksAdded;
                else
                    model.Books = new List<BookViewModel>();
                using (DbContextTransaction dbTran = db.Database.BeginTransaction())
                {
                    try
                    {
                        Category category = new Category();
                        category.CategoryName = model.Name;
                        db.Categories.Add(category);
                      
                        if (BooksAdded != null)
                        {
                            foreach (var book in BooksAdded)
                            {
                                Book b = await db.Books.FindAsync(book.Id);
                                category.Books.Add(b);                               
                            }
                        }
                        await db.SaveChangesAsync();
                        dbTran.Commit();
                        BooksAdded = null;
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

                        dbTran.Rollback();

                        List<BookViewModel> pendingBooks = await GetPendingBooks();
                        ViewBag.BookId = new SelectList(pendingBooks, "Id", "Title");

                        return View(model);
                    }
                }
            }

            return View(model);
        }

        // GET: Categories/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = await db.Categories.FindAsync(id);
            if (category == null)
            {
                return HttpNotFound();
            }

            CategoryViewModel model = GetModel(category);
            BooksAdded = model.Books;
            List<BookViewModel> allBooks = await GetPendingBooks();
            
            ViewBag.BookId = new SelectList(allBooks, "Id", "Title");

            return View(model);
        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(CategoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (BooksAdded != null)
                    model.Books = BooksAdded;
                else
                    model.Books = new List<BookViewModel>();
                using (DbContextTransaction dbTran = db.Database.BeginTransaction())
                {
                    try
                    {
                        Category category = await db.Categories.FindAsync(model.Id);
                        category.CategoryName = model.Name;
                        db.Entry(category).State = EntityState.Modified;

                        category.Books.Clear();

                        if (BooksAdded != null)
                        {
                            foreach (var book in BooksAdded)
                            {
                                Book b = await db.Books.FindAsync(book.Id);
                                category.Books.Add(b);
                            }
                        }
                        await db.SaveChangesAsync();
                        dbTran.Commit();
                        BooksAdded = null;
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

                        dbTran.Rollback();

                        List<BookViewModel> pendingBooks = await GetPendingBooks();
                        ViewBag.BookId = new SelectList(pendingBooks, "Id", "Title");

                        return View(model);
                    }
                }
            }

            return View(model);
        }

        // GET: Categories/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = await db.Categories.FindAsync(id);
            if (category == null)
            {
                return HttpNotFound();
            }

            CategoryViewModel model = GetModel(category);
            BooksAdded = model.Books;
            return View(model);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            try
            {
                Category category = await db.Categories.FindAsync(id);
                db.Categories.Remove(category);
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

                return RedirectToAction("Delete", new
                {
                    id = id
                });
            }
        }

        public List<BookViewModel> BooksAdded
        {
            get
            {
                if (Session["BooksAdded"] != null)
                    return Session["BooksAdded"] as List<BookViewModel>;
                else
                    return null;
            }
            set
            {
                Session["BooksAdded"] = value;
            }
        }
        public JsonResult AddBook(BookViewModel book)
        {
            if (BooksAdded == null)
                BooksAdded = new List<BookViewModel>();

            BooksAdded.Add(book);
            var booksPartial = RenderRazorViewToString(this.ControllerContext, "_Books", BooksAdded);
            return Json(new { Books = booksPartial }, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> DeleteBook(int index)
        {
            BooksAdded.RemoveAt(index);
            
            var booksPartial = RenderRazorViewToString(this.ControllerContext, "_Books", BooksAdded);
            List<BookViewModel> pendingBooks = await GetPendingBooks();

            return Json(new { Books = booksPartial, AllBooks = pendingBooks }, JsonRequestBehavior.AllowGet);
        }

        private string RenderRazorViewToString(ControllerContext controllerContext, String viewName, Object model)
        {
            controllerContext.Controller.ViewData.Model = model;

            using (var sw = new StringWriter())
            {
                var ViewResult = ViewEngines.Engines.FindPartialView(controllerContext, viewName);
                var ViewContext = new ViewContext(controllerContext, ViewResult.View, controllerContext.Controller.ViewData, controllerContext.Controller.TempData, sw);
                ViewResult.View.Render(ViewContext, sw);
                ViewResult.ViewEngine.ReleaseView(controllerContext, ViewResult.View);
                return sw.GetStringBuilder().ToString();
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
