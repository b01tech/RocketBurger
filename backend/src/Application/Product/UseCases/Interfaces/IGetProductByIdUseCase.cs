using Application.Product.Dtos;
using Core.Shared.Result;

namespace Application.Product.UseCases.Interfaces;

public interface IGetProductByIdUseCase
{
    Task<Result<ProductResponse>> ExecuteAsync(long id);
}
