using Core.Entities;
using Core.Repositories;
using Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositories;

internal class ProductRepository(RocketBugerDbContext dbContext) : IProductRepository
{
    public async Task<Product?> GetByIdAsync(long id)
    {
        return await dbContext.Products
            .Include(p => p.Category)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<List<Product>> GetAllActiveAsync(int page, int pageSize = 25)
    {
        return await dbContext.Products
            .AsNoTracking()
            .Include(p => p.Category)
            .Where(p => p.IsActive)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<Product> AddAsync(Product product)
    {
        await dbContext.Products.AddAsync(product);
        return product;
    }

    public Task<bool> UpdateAsync(Product product)
    {
        dbContext.Products.Update(product);
        return Task.FromResult(true);
    }

    public Task<bool> UpdatePriceAsync(Product product, decimal newPrice)
    {
        var result = product.UpdatePrice(newPrice);
        if (result.IsSuccess)
        {
            dbContext.Products.Update(product);
            return Task.FromResult(true);
        }
        return Task.FromResult(false);
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var product = await dbContext.Products.FindAsync(id);
        if (product is null) return false;

        product.Deactivate();
        dbContext.Products.Update(product);

        return true;
    }

    public async Task<bool> ActivateAsync(long id)
    {
        var product = await dbContext.Products.FindAsync(id);
        if (product is null) return false;

        product.Activate();
        dbContext.Products.Update(product);
        return true;
    }

    public async Task<bool> ChangeStockAsync(long id, int quantity)
    {
        var product = await dbContext.Products.FindAsync(id);
        if (product is null) return false;

        if (quantity > 0)
        {
            var result = product.AddStock(quantity);
            if (!result.IsSuccess) return false;
        }
        else if (quantity < 0)
        {
            var result = product.RemoveStock(Math.Abs(quantity));
            if (!result.IsSuccess) return false;
        }

        dbContext.Products.Update(product);
        return true;
    }
}
