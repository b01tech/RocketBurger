using Application.Product.Dtos;
using Core.Shared.Result;

namespace Application.Product.UseCases.Interfaces;

public interface IUpdateProductUseCase
{
    Task<Result<ProductResponse>> ExecuteAsync(UpdateProductRequest request);
}
