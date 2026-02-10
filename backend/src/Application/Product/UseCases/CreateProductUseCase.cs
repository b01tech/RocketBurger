using System.Net;
using Application.Product.Dtos;
using Application.Product.UseCases.Interfaces;
using Core.Repositories;
using Core.Shared.Result;

namespace Application.Product.UseCases;

internal class CreateProductUseCase(IProductRepository repository, ICategoryRepository categoryRepository, IUnitOfWork unitOfWork) : ICreateProductUseCase
{
    public async Task<Result<ProductResponse>> ExecuteAsync(CreateProductRequest request)
    {
        var category = await categoryRepository.GetCategoryByIdAsync(request.CategoryId);
        if (category is null)
            return Result<ProductResponse>.Failure(Core.Shared.Error.ErrorMessages.CATEGORY_NOT_FOUND, (int)HttpStatusCode.NotFound);

        var resultPrice = Core.Shared.ValueObjects.Price.Create(request.Price);
        if (!resultPrice.IsSuccess)
            return Result<ProductResponse>.Failure(resultPrice.Error!.Errors, (int)HttpStatusCode.BadRequest);

        var resultProduct = Core.Entities.Product.Create(request.Name, request.Description ?? string.Empty, request.ImageUrl ?? string.Empty, resultPrice.Data!, category.Id);
        if (!resultProduct.IsSuccess)
            return Result<ProductResponse>.Failure(resultProduct.Error!.Errors, (int)HttpStatusCode.BadRequest);

        var product = resultProduct.Data!;
        if (request.StockQuantity > 0)
        {
            var resultStock = product.AddStock(request.StockQuantity);
            if (!resultStock.IsSuccess)
                return Result<ProductResponse>.Failure(resultStock.Error!.Errors, (int)HttpStatusCode.BadRequest);
        }

        var addedProduct = await repository.AddAsync(product);
        await unitOfWork.CommitAsync();

        return new ProductResponse(
            product.Id,
            product.Name,
            product.Description ?? string.Empty,
            product.Price.Value,
            product.ImageUrl ?? string.Empty,
            product.CategoryId,
            category.Name,
            product.IsActive,
            product.Stock.Quantity
        );
    }
}
