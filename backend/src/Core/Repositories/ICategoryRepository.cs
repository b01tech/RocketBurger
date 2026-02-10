using Core.Entities;

namespace Core.Repositories;

public interface ICategoryRepository
{
    Task<IEnumerable<Category>> GetAllCategoriesAsync();
    Task<bool> CategoryExistsAsync(string name);
    Task<Category?> GetCategoryByIdAsync(long id);
    Task<Category> AddCategoryAsync(Category category);
}
