using Core.Entities;

namespace Core.Repositories;

public interface IProductRepository
{
    Task<Product?> GetByIdAsync(long id);
    Task<List<Product>> GetAllActiveAsync(int page, int pageSize = 25);

    Task<Product> AddAsync(Product product);
    Task<bool> UpdateAsync(Product product);
    Task<bool> UpdatePriceAsync(Product product, decimal newPrice);
    Task<bool> DeleteAsync(long id);
    Task<bool> ActivateAsync(long id);
    Task<bool> ChangeStockAsync(long id, int quantity);

}
