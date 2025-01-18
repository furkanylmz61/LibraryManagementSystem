using LibraryManagementSystem.Repositories.category;

namespace LibraryManagementSystem.Services.Category;

public class CategoryService: ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;

    public CategoryService(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<IEnumerable<Models.Category>> GetAllCategoriesAsync()
    {
        return await _categoryRepository.GetAllAsync();
    }

    public async Task<Models.Category> GetCategoryByIdAsync(Guid id)
    {
        return await _categoryRepository.GetByIdAsync(id);
    }

    public async Task AddCategoryAsync(Models.Category category)
    {
        category.CategoryId = Guid.NewGuid();
        await _categoryRepository.AddAsync(category);
        await _categoryRepository.SaveChangesAsync();
    }

    public async Task UpdateCategoryAsync(Models.Category category)
    {
        _categoryRepository.Update(category);
        await _categoryRepository.SaveChangesAsync();
    }

    public async Task DeleteCategoryAsync(Guid id)
    {
        var category = await _categoryRepository.GetByIdAsync(id);
        if (category != null)
        {
            _categoryRepository.Remove(category);
            await _categoryRepository.SaveChangesAsync();
        }
    }
}