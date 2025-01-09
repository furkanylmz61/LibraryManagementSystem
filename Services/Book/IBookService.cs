namespace LibraryManagementSystem.Services.Book;

public interface IBookService
{
    Task<IEnumerable<Models.Book>> GetAllBooksAsync();
    Task<Models.Book> GetBookByIdAsync(Guid bookId);
    Task AddBookAsync(Models.Book book);
    Task UpdateBookAsync(Models.Book book);
    Task DeleteBookAsync(Guid bookId);
    Task<IEnumerable<Models.Book>> GetBooksByAuthorAsync(string authorName);
}