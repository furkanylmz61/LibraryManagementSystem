using LibraryManagementSystem.Models;
using LibraryManagementSystem.Services.Book;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging; 
using System;

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
        public async Task<IActionResult> Create(Book book)
        {
            // Model State kontrolü
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("ModelState is invalid while creating a new Book. Errors: {errors}", 
                    ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
                return View(book);
            }

            // Try-Catch ile hata yakalama durumu gerçekleştiriyoruz.
            try
            {
                await _bookService.AddBookAsync(book);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating a new Book.");
                
                // burada hatayı sayfaya basıyoruz
                ModelState.AddModelError("", "An error occurred while saving the book to the database.");
                return View(book);
            }

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

        // GET: /Book/Delete/{id}
        public async Task<IActionResult> Delete(Guid id)
        {
            var book = await _bookService.GetBookByIdAsync(id);
            if (book == null)
                return NotFound();

            return View(book);
        }

        // POST: /Book/Delete/{id}
        [HttpPost, ActionName("Delete")]
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
                // Tekrar view göstermek için
                var book = await _bookService.GetBookByIdAsync(id);
                return View("Delete", book);
            }

            return RedirectToAction(nameof(Index));
        }
    }

