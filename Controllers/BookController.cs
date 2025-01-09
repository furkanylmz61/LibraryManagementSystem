using LibraryManagementSystem.Models;
using LibraryManagementSystem.Services.Book;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Controllers;

public class BookController: Controller
{
    private readonly IBookService _bookService;

    public BookController(IBookService bookService)
    {
        _bookService = bookService;
    }//BookService interface'ini burda DI ediyoruz.
    
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
        if (!ModelState.IsValid)
        {
            return View(book);
        }

        await _bookService.AddBookAsync(book);
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

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Book book, Guid id)
    {
        if (id == null)
        {
            return NotFound();
        }

        if (!ModelState.IsValid)
        {
            return View(book);
        }
        await _bookService.UpdateBookAsync(book);
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
        await _bookService.DeleteBookAsync(id);
        return RedirectToAction(nameof(Index));
    }
}
