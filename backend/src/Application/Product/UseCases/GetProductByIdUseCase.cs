using System.Net;
using Application.Product.Dtos;
using Application.Product.UseCases.Interfaces;
using Core.Repositories;
using Core.Shared.Result;

namespace Application.Product.UseCases;

internal class GetProductByIdUseCase(IProductRepository repository) : IGetProductByIdUseCase
{
    public async Task<Result<ProductResponse>> ExecuteAsync(long id)
    {
        var product = await repository.GetByIdAsync(id);
        if (product is null)
            return Result<ProductResponse>.Failure("Produto não encontrado", (int)HttpStatusCode.NotFound);

        return new ProductResponse(
            product.Id,
            product.Name,
            product.Description ?? string.Empty,
            product.Price.Value,
            product.ImageUrl ?? string.Empty,
            product.CategoryId,
            product.Category?.Name ?? string.Empty,
            product.IsActive,
            product.Stock.Quantity
        );
    }
}
