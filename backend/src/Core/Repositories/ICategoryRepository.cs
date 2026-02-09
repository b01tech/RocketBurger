using Core.Entities;

namespace Core.Repositories;

public interface ICategoryRepository
{
    Task<bool> CategoryExistsAsync(string name);
    Task<Category> AddCategoryAsync(Category category);
}
