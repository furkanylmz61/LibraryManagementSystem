using LibraryManagementSystem.Models;
using LibraryManagementSystem.Services.Book;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Controllers;

    public class BookController : Controller
    {
        private readonly IBookService _bookService;
        private readonly ILogger<BookController> _logger;

        public BookController(IBookService bookService, ILogger<BookController> logger)
        {
            _bookService = bookService;
            _logger = logger;
        }

        //Get /Book
        public async Task<IActionResult> Index()
        {
            var books = await _bookService.GetAllBooksAsync();
            return View(books);
        }

        //boş form göster
        public IActionResult Create()
        {
            return View();
        }


        
        [HttpPost] 
        [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Book book, IFormFile formFile)
    {
    // Desteklenen dosya uzantıları
    var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };

    if (formFile != null && formFile.Length > 0)
    {
        var extension = Path.GetExtension(formFile.FileName).ToLowerInvariant();

        if (!allowedExtensions.Contains(extension))
        {
            ModelState.AddModelError("", "Lütfen geçerli bir resim uzantısı yükleyiniz (.jpg, .jpeg veya .png).");
        }
        else
        {
            // Dosya adı oluştur
            var randomFileName = $"{Guid.NewGuid()}{extension}";

            // Dosya yolu oluştur
            var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads", randomFileName);

            try
            {
                // uploads klasörünün varlığını kontrol et, yoksa oluştur
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
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Dosya yüklenirken bir hata oluştu.");
                ModelState.AddModelError("", "Dosya yüklenirken bir hata oluştu. Lütfen tekrar deneyin.");
            }
        }
    }
    else
    {
        ModelState.AddModelError("", "Lütfen bir resim dosyası seçiniz.");
    }

    // ModelState kontrolü
    if (!ModelState.IsValid)
    {
        return View(book);
    }

    // Kitap ekleme işlemi
    try
    {
        await _bookService.AddBookAsync(book);
    }
    catch (Exception ex)
    {
        _logger.LogError(ex, "Veritabanına kitap eklenirken bir hata oluştu.");
        ModelState.AddModelError("", "Kitap eklenirken bir hata oluştu.");
        return View(book);
    }

    // Başarılıysa Index sayfasına yönlendir
    return RedirectToAction(nameof(Index));
}




        //Detail 
        public async Task<IActionResult> Details(Guid id)
        {
            var books = await _bookService.GetBookByIdAsync(id);
            if (books == null)
            {
                return NotFound();
            }

            return View(books);
        }

        // GET: /Book/Edit/{id}
        public async Task<IActionResult> Edit(Guid id)
        {
            var book = await _bookService.GetBookByIdAsync(id);
            if (book == null)
                return NotFound();

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
                return View(book);
            }

            try
            {
                await _bookService.UpdateBookAsync(book);
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
            var book = await _bookService.GetBookByIdAsync(id);
            if (book == null)
                return NotFound();

            return View(book); 
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            try
            {
                await _bookService.DeleteBookAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting Book ID: {id}", id);
                ModelState.AddModelError("", "An error occurred while deleting the book.");


                var book = await _bookService.GetBookByIdAsync(id);
                return View("Delete", book);
            }

            return RedirectToAction(nameof(Index));
        }

    }

