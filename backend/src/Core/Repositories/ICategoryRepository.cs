using Core.Entities;

namespace Core.Repositories;

public interface ICategoryRepository
{
    Task<Category> AddCategoryAsync(Category category);
}
