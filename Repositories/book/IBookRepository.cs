using LibraryManagementSystem.Models;
using LibraryManagementSystem.Repositories.generic;

namespace LibraryManagementSystem.Repositories.book;

public interface IBookRepository : IGenericRepository<Models.Book>
{
    //book'a özel metodlar eklenebilir bu alana
    Task<IEnumerable<Book>> GetBooksByAuthorAsync(string authorName);
}