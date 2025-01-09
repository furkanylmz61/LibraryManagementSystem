using LibraryManagementSystem.Repositories.book;

namespace LibraryManagementSystem.Services.Book;

public class BookService : IBookService
{
    private readonly IBookRepository _bookRepository;

    public BookService(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }


    public async Task<IEnumerable<Models.Book>> GetAllBooksAsync()
    {
        return await _bookRepository.GetAllAsync();
    }
    public async Task<Models.Book> GetBookByIdAsync(Guid id)
    {
        return await _bookRepository.GetByIdAsync(id);
    }

    public async Task AddBookAsync(Models.Book book)
    {
        book.BookId = Guid.NewGuid();
        book.CreatedDate = DateTime.Now;

        await _bookRepository.AddAsync(book);
        await _bookRepository.SaveChangesAsync();
    }

    public async Task UpdateBookAsync(Models.Book book)
    {
        book.UpdatedDate = DateTime.Now;
        _bookRepository.Update(book);
        await _bookRepository.SaveChangesAsync();
    }

    public async Task DeleteBookAsync(Guid bookId)
    {
        var book = await _bookRepository.GetByIdAsync(bookId);
        if (book !=null)
        {
            _bookRepository.Remove(book);
            await _bookRepository.SaveChangesAsync();
        }
        else
        {
            throw new Exception($"Book (ID: {bookId}) not found!");

        }
    }

    public async Task<IEnumerable<Models.Book>> GetBooksByAuthorAsync(string authorName)
    {
        return await _bookRepository.GetBooksByAuthorAsync(authorName);
    }
}