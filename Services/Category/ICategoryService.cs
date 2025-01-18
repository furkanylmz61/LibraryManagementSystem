namespace LibraryManagementSystem.Services.Category;

public interface ICategoryService
{
    Task<IEnumerable<Models.Category>> GetAllCategoriesAsync();
    Task<Models.Category> GetCategoryByIdAsync(Guid id);
    Task AddCategoryAsync(Models.Category category);
    Task UpdateCategoryAsync(Models.Category category);
    Task DeleteCategoryAsync(Guid id);
}