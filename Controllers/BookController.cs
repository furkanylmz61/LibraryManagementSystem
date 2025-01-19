using LibraryManagementSystem.Data;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Services.Book;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Controllers;

    public class BookController : Controller
    {
        private readonly IBookService _bookService;
        private readonly ILogger<BookController> _logger;
        private readonly ApplicationDbContext _context;

        public BookController(IBookService bookService, ILogger<BookController> logger, ApplicationDbContext context)
        {
            _bookService = bookService;
            _logger = logger;
            _context = context;
        }

        //Get /Book
        public async Task<IActionResult> Index()
        {
            var books = await _context.Books
                .Include(b => b.Category)
                .ToListAsync();
            return View(books);
        }

        //boş form göster
        public IActionResult Create()
        {
            LoadCategoriesToViewBag();
            return View();
        }
        

[HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> Create(Book book, IFormFile formFile)
{
    
    if (!ModelState.IsValid)
    {
        _logger.LogWarning("Form validation error: {Errors}", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
        LoadCategoriesToViewBag(); 
        return View(book);
    }
    // Desteklenen dosya uzantıları
    var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };

    if (formFile != null && formFile.Length > 0)
    {
        var extension = Path.GetExtension(formFile.FileName).ToLowerInvariant();

        if (!allowedExtensions.Contains(extension))
        {
            ModelState.AddModelError("", "please select valid image file (.jpg, .jpeg veya .png).");
            _logger.LogWarning("Invalid file extension loaded: {Extension}", extension);
        }
        else
        {
            var randomFileName = $"{Guid.NewGuid()}{extension}";
            var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads", randomFileName);

            try 
            {
                var uploadDir = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");
                if (!Directory.Exists(uploadDir))
                {
                    Directory.CreateDirectory(uploadDir);
                }

                // Dosyayı kaydet
                using (var stream = new FileStream(uploadPath, FileMode.Create))
                {
                    await formFile.CopyToAsync(stream);
                }

                // Kitap modeline resim yolunu ata
                book.CoverImage = randomFileName;
                _logger.LogInformation("Image successfully uploaded and saved: {FileName}", randomFileName);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding a book");
                ModelState.AddModelError("", "An error occurred while adding a book");
                LoadCategoriesToViewBag(); 
                return View(book);
            }
        }
    }
    else
    {
        ModelState.AddModelError("", "please select an image file");
        _logger.LogWarning("The user submitted the form without selecting an image.");
    }

    

    //ADD book
    try
    {
        _logger.LogInformation("Book addition process has been initiated {Book}", book);
        await _bookService.AddBookAsync(book);
        _logger.LogInformation("Book successfully added {BookId}", book.BookId);
    }
    catch (Exception ex)
    {
        _logger.LogError(ex, "While adding the book an error occured");
        ModelState.AddModelError("", "While adding the book an error occured.");
        LoadCategoriesToViewBag();
        return View(book);
    }

    // if is success return ındex page
    return RedirectToAction(nameof(Index));
}




        //Detail 
        public async Task<IActionResult> Details(Guid id)
        {
            var book = await _context.Books
                .Include(b => b.Category)
                .FirstOrDefaultAsync(b => b.BookId == id);

            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // GET: /Book/Edit/{id}
        public async Task<IActionResult> Edit(Guid id)
        {
            var book = await _context.Books
                .Include(b => b.Category)
                .FirstOrDefaultAsync(b => b.BookId == id);

            if (book == null)
            {
                return NotFound();
            }

            LoadCategoriesToViewBag();
            return View(book);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Book book, Guid id)
        {
            if (id == null) 
                return NotFound();

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("ModelState is invalid while editing Book ID: {id}", id);
                LoadCategoriesToViewBag();
                return View(book);
            }

            try
            {
                _context.Books.Update(book);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while editing Book ID: {id}", id);
                ModelState.AddModelError("", "An error occurred while updating the book.");
                return View(book);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var book = await _context.Books
                .Include(b => b.Category)
                .FirstOrDefaultAsync(b => b.BookId == id);

            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            try
            {
                var book = await _context.Books.FindAsync(id);
                if (book != null)
                {
                    _context.Books.Remove(book);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting Book ID: {id}", id);
                ModelState.AddModelError("", "An error occurred while deleting the book.");


                var book = await _context.Books
                    .Include(b => b.Category)
                    .FirstOrDefaultAsync(b => b.BookId == id);
                return View("Delete", book);
            }

            return RedirectToAction(nameof(Index));
        }
        
        private void LoadCategoriesToViewBag()
        {
            var categories = _context.Categories.OrderBy(c => c.Name).ToList();
            if (!categories.Any())
            {
                _logger.LogWarning("Category table empty!");
            }
            ViewBag.CategoryList = new SelectList(categories, "CategoryId", "Name");
        }



    }

