using Application.Product.Dtos;
using Core.Shared.Result;

namespace Application.Product.UseCases.Interfaces;

public interface ICreateProductUseCase
{
    Task<Result<ProductResponse>> ExecuteAsync(CreateProductRequest request);
}
