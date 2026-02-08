using Core.Entities;
using Core.Repositories;
using Infra.Data;

namespace Infra.Repositories;

internal class CategoryRepository(RocketBugerDbContext dbContext) : ICategoryRepository
{
    public async Task<Category> AddCategoryAsync(Category category)
    {
        await dbContext.Categories.AddAsync(category);
        return category;
    }
}
