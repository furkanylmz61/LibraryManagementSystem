using LibraryManagementSystem.Models;
using LibraryManagementSystem.Services.Book;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Controllers;

public class HomeController : Controller
{
    private readonly IBookService _bookService;

    public HomeController(IBookService bookService)
    {
        _bookService = bookService;
    }

    

    public async Task<IActionResult> Index()
    {
        var allBooks = await _bookService.GetAllBooksAsync();
        var featuredBooks = allBooks.Take(4); 

        var model = new HomeViewModel
        {
            FeaturedBooks = featuredBooks
        };

        return View(model);
    }
    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return null;
    }
    
    

}