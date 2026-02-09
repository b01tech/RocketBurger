using Core.Entities;
using Core.Repositories;
using Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositories;

internal class CategoryRepository(RocketBugerDbContext dbContext) : ICategoryRepository
{
    public async Task<bool> CategoryExistsAsync(string name)
    {
        return await dbContext.Categories.AnyAsync(c => c.Name == name);
    }
    public async Task<Category?> GetCategoryByIdAsync(long id)
    {
        return await dbContext.Categories.FindAsync(id);
    }

    public async Task<Category> AddCategoryAsync(Category category)
    {
        await dbContext.Categories.AddAsync(category);
        return category;
    }
}
