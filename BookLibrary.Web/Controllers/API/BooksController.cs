using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using BookLibrary.Model;
using BookLibrary.Web.Models;

namespace BookLibrary.Web.Controllers.API
{
    public class BooksController : ApiController
    {
        private BookLibraryEntities db = new BookLibraryEntities();

        [HttpGet]
        [Route("api/Books/GetAll")]
        public async Task<IHttpActionResult> GetAll()
        {
            List<Book> books = await db.Books.ToListAsync();
            List<BookViewModel> result = new List<BookViewModel>();

            foreach (var book in books)
            {
                BookViewModel b = new BookViewModel() { Id = book.BookId, Title = book.Title, AuthorId = book.AuthorId, AuthorName = book.Author.AuthorName, Price = book.Price, Editorial = book.Editorial };
                result.Add(b);
            }
            return Ok(result);
        }

        [HttpGet]
        [Route("api/Books/GetByCategory/{CategoryName}")]
        public async Task<IHttpActionResult> GetByCategory(string CategoryName)
        {
            List<Book> books = await db.Books.Where(b => b.Categories.Any(c => c.CategoryName == CategoryName)).ToListAsync();
            List<BookViewModel> result = new List<BookViewModel>();

            foreach (var book in books)
            {
                BookViewModel b = new BookViewModel() { Id = book.BookId, Title = book.Title, AuthorId = book.AuthorId, AuthorName = book.Author.AuthorName, Price = book.Price, Editorial = book.Editorial };
                result.Add(b);
            }
            return Ok(result);
        }

        [HttpGet]
        [Route("api/Books/GetByAuthor/{AuthorName}")]
        public async Task<IHttpActionResult> GetByAuthor(string AuthorName)
        {
            List<Book> books = await db.Books.Where(b => b.Author.AuthorName == AuthorName).ToListAsync();
            List<BookViewModel> result = new List<BookViewModel>();

            foreach (var book in books)
            {
                BookViewModel b = new BookViewModel() { Id = book.BookId, Title = book.Title, AuthorId = book.AuthorId, AuthorName = book.Author.AuthorName, Price = book.Price, Editorial = book.Editorial };
                result.Add(b);
            }
            return Ok(result);
        }

        [HttpGet]
        [Route("api/Books/GetByCategoryId/{CategoryId}")]
        public async Task<IHttpActionResult> GetByCategoryId(int CategoryId)
        {
            List<Book> books = await db.Books.Where(b => b.Categories.Any(c => c.CategoryId == CategoryId)).ToListAsync();
            List<BookViewModel> result = new List<BookViewModel>();

            foreach (var book in books)
            {
                BookViewModel b = new BookViewModel() { Id = book.BookId, Title = book.Title, AuthorId = book.AuthorId, AuthorName = book.Author.AuthorName, Price = book.Price, Editorial = book.Editorial };
                result.Add(b);
            }
            return Ok(result);
        }

        [HttpGet]
        [Route("api/Books/GetByAuthorId/{AuthorId}")]
        public async Task<IHttpActionResult> GetByAuthorId(int AuthorId)
        {
            List<Book> books = await db.Books.Where(b => b.AuthorId == AuthorId).ToListAsync();
            List<BookViewModel> result = new List<BookViewModel>();

            foreach (var book in books)
            {
                BookViewModel b = new BookViewModel() { Id = book.BookId, Title = book.Title, AuthorId = book.AuthorId, AuthorName = book.Author.AuthorName, Price = book.Price, Editorial = book.Editorial };
                result.Add(b);
            }
            return Ok(result);
        }
    }
}