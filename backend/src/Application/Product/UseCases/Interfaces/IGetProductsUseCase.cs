using Application.Product.Dtos;
using Application.Shared;
using Core.Shared.Result;

namespace Application.Product.UseCases.Interfaces;

public interface IGetProductsUseCase
{
    Task<Result<PaginationResponse<ProductResponse>>> ExecuteAsync(int page, int pageSize);
}
