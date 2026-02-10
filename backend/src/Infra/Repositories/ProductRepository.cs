using Core.Entities;
using Core.Repositories;
using Infra.Data;

namespace Infra.Repositories;

internal class ProductRepository(RocketBugerDbContext dbContext) : IProductRepository
{
    public Task<Product?> GetByIdAsync(long id)
    {
        throw new NotImplementedException();
    }

    public Task<List<Product>> GetAllActiveAsync(int page, int pageSize = 25)
    {
        throw new NotImplementedException();
    }

    public Task<Product> AddAsync(Product product)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateAsync(Product product)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdatePriceAsync(Product product, decimal newPrice)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAsync(long id)
    {
        throw new NotImplementedException();
    }

    public Task<bool> ActivateAsync(long id)
    {
        throw new NotImplementedException();
    }

    public Task<bool> ChangeStockAsync(long id, int quantity)
    {
        throw new NotImplementedException();
    }
}
