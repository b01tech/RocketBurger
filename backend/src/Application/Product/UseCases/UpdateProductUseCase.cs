using System.Net;
using Application.Product.Dtos;
using Application.Product.UseCases.Interfaces;
using Core.Repositories;
using Core.Shared.Result;

namespace Application.Product.UseCases;

internal class UpdateProductUseCase(IProductRepository repository, ICategoryRepository categoryRepository, IUnitOfWork unitOfWork) : IUpdateProductUseCase
{
    public async Task<Result<ProductResponse>> ExecuteAsync(UpdateProductRequest request)
    {
        var product = await repository.GetByIdAsync(request.Id);
        if (product is null)
            return Result<ProductResponse>.Failure("Produto não encontrado", (int)HttpStatusCode.NotFound);

        var category = await categoryRepository.GetCategoryByIdAsync(request.CategoryId);
        if (category is null)
            return Result<ProductResponse>.Failure("Categoria não encontrada", (int)HttpStatusCode.NotFound);

        var resultUpdate = product.Update(request.Name, request.Description ?? string.Empty, request.ImageUrl ?? string.Empty, request.CategoryId);
        if (!resultUpdate.IsSuccess)
            return Result<ProductResponse>.Failure(resultUpdate.Error!.Errors, (int)HttpStatusCode.BadRequest);

        var resultPrice = product.UpdatePrice(request.Price);
        if (!resultPrice.IsSuccess)
            return Result<ProductResponse>.Failure(resultPrice.Error!.Errors, (int)HttpStatusCode.BadRequest);

        await repository.UpdateAsync(product);
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
