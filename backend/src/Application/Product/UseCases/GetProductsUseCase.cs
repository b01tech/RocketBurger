using Application.Product.Dtos;
using Application.Product.UseCases.Interfaces;
using Application.Shared;
using Core.Repositories;
using Core.Shared.Result;

namespace Application.Product.UseCases;

internal class GetProductsUseCase(IProductRepository repository) : IGetProductsUseCase
{
    public async Task<Result<PaginationResponse<ProductResponse>>> ExecuteAsync(int page, int pageSize)
    {
        var products = await repository.GetAllActiveAsync(page, pageSize);
        
        var response = products.Select(product => new ProductResponse(
            product.Id,
            product.Name,
            product.Description ?? string.Empty,
            product.Price.Value,
            product.ImageUrl ?? string.Empty,
            product.CategoryId,
            product.Category?.Name ?? string.Empty,
            product.IsActive,
            product.Stock.Quantity
        )).ToList();

        // TODO: Implement total count fetching in repository
        int totalItems = response.Count; 

        return new PaginationResponse<ProductResponse>(page, pageSize, totalItems, response);
    }
}
