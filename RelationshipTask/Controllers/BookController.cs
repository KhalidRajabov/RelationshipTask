using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RelationshipTask.DAL;
using RelationshipTask.Models;
using System.Collections.Generic;
using System.Linq;

namespace RelationshipTask.Controllers
{
    public class BookController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public BookController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public IActionResult Index()
        {
            List<Book> book = new List<Book>();
            book = _context.Books.Include(a_i=>a_i.BookAuthors).ThenInclude(a=>a.Authors)
                .Include(g_i=>g_i.BookGenres).ThenInclude(g=>g.Genres).ToList();
            return View(book);
        }
        public IActionResult Create()
        {
            ViewBag.Authors = new SelectList(_context.Authors.ToList(), "Id", "Name");
            ViewBag.Genres= new SelectList(_context.Genre.ToList(), "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Book book)
        {
            if (!ModelState.IsValid) return View();
            Book newbook = new Book
            {
                Name = book.Name,
                Price = book.Price
            };
            List<BookAuthor> bookAuthors = new List<BookAuthor>();

            foreach (int item in book.AuthorIds)
            {
                BookAuthor bookAuthor = new BookAuthor();
                bookAuthor.AuthorId=item;
                bookAuthor.BookId = newbook.Id;
                bookAuthors.Add(bookAuthor);
            }
            newbook.BookAuthors=bookAuthors;
            List<BookGenre> bookGenres = new List<BookGenre>();

            foreach (int item in book.GenreIds)
            {
                BookGenre bookGenre = new BookGenre();
                bookGenre.GenreId = item;
                bookGenre.BookId = newbook.Id;
                bookGenres.Add(bookGenre);
            }
            newbook.BookGenres = bookGenres; ;
            _context.Add(newbook);
            _context.SaveChanges();
            return RedirectToAction("index");
        }

    }
}
