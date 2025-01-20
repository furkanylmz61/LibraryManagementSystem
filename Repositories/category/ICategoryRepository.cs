using LibraryManagementSystem.Models;
using LibraryManagementSystem.Repositories.generic;

namespace LibraryManagementSystem.Repositories.category;

public interface ICategoryRepository : IGenericRepository<Category>
{
    Task<IEnumerable<Category>> GetAllAsync(string includeProperties = "");

}