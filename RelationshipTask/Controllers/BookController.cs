using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RelationshipTask.DAL;
using RelationshipTask.Extensions;
using RelationshipTask.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

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
                .Include(g_i=>g_i.BookGenres).ThenInclude(g=>g.Genres).Include(i=>i.Images).ToList();
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
            ViewBag.Authors = new SelectList(_context.Authors.ToList(), "Id", "Name");
            ViewBag.Genres = new SelectList(_context.Genre.ToList(), "Id", "Name");
             
            List<Image> Images = new List<Image>();

            foreach (var item in book.Photos)
            {
                if (item == null)
                {
                    ModelState.AddModelError("Photo", "Do not leave it empty");
                    return View();
                }

                if (!item.IsImage())
                {
                    ModelState.AddModelError("Photo", "Do not leave it empty");
                    return View();
                }
                if (item.ValidSize(10000))
                {
                    ModelState.AddModelError("Photo", "Image size can not be large");
                    return View();
                }
                Image image = new Image();
                image.ImageUrl = item.SaveImage(_env, "img");
                Images.Add(image);
            }
            if (book.GenreIds==null)
            {
                ModelState.AddModelError("GenreIds", "Choose a genre");
                return View();
            }
            if (book.AuthorIds== null)
            {
                ModelState.AddModelError("Author", "Choose an author");
                return View();
            }


            if (!ModelState.IsValid) return View();
            Book newbook = new Book
            {
                Name = book.Name.ToUpper(),
                Price = book.Price,
                Images = Images
                
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
        public async Task<IActionResult> Detail(int? id)
        {
            if(id== null) return NotFound();
            Book book = await _context.Books.Include(a_i => a_i.BookAuthors).ThenInclude(a => a.Authors)
                .Include(g_i => g_i.BookGenres).ThenInclude(g => g.Genres).Include(i=>i.Images).FirstOrDefaultAsync(c => c.Id == id);
            if (book== null) return NotFound();
            return View(book);
        }



        public async Task<IActionResult> Update(int? id)
        {
            ViewBag.Authors = new SelectList(_context.Authors.ToList(), "Id", "Name");
            ViewBag.Genres = new SelectList(_context.Genre.ToList(), "Id", "Name");
            if (id == null) return NotFound();
            Book book = await _context.Books.Include(a_i => a_i.BookAuthors).ThenInclude(a => a.Authors)
                .Include(g_i => g_i.BookGenres).ThenInclude(g => g.Genres).Include(i => i.Images).FirstOrDefaultAsync(c => c.Id == id);
            if (book == null) return NotFound();
            return View(book);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, Book book)
        {
            ViewBag.Authors = new SelectList(_context.Authors.ToList(), "Id", "Name");
            ViewBag.Genres = new SelectList(_context.Genre.ToList(), "Id", "Name");

            if (!ModelState.IsValid) return View();
            if (id == null) return NotFound();


            Book dbbook= await _context.Books.FindAsync(id);
            Image dbimage = new Image();
            if (dbbook == null) return NotFound();
            List<Image> images = new List<Image>();
            foreach (var item in book.Photos)
            {
                if (item == null)
                {
                    dbimage.ImageUrl = dbimage.ImageUrl;
                }
                else
                {

                    if (!item.IsImage())
                    {
                        ModelState.AddModelError("Photo", "Choose images only");
                        return View();
                    }
                    if (item.ValidSize(20000))
                    {
                        ModelState.AddModelError("Photo", "Image size can not be large");
                        return View();
                    }
                    string oldImg = dbimage.ImageUrl;
                    string path = Path.Combine(_env.WebRootPath, "img", oldImg);
                    dbimage.ImageUrl = item.SaveImage(_env, "img");
                    Helper.Helper.DeleteImage(path);

                }
            }




            dbbook.Name = book.Name;
            dbbook.Price = book.Price;
            dbbook.GenreIds = book.GenreIds;
            dbbook.AuthorIds = book.AuthorIds;

            _context.Books.Add(dbbook);
            await _context.SaveChangesAsync();
            return RedirectToAction("index");
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            Book book= await _context.Books.FindAsync(id);
            if (book == null) return NotFound();
            
            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
            return RedirectToAction("index");
        }



        

    }
}
