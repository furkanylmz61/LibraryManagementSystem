using LibraryManagementSystem.Data;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Repositories.generic;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Repositories.book;

public class BookRepository: GenericRepository<Models.Book>, IBookRepository
{
    public BookRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Book>> GetBooksByAuthorAsync(string authorName)
    {
        return await _context.Books
            .Where(b => b.Author.Contains(authorName))
            .ToListAsync();
    }
}