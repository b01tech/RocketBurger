using Core.Shared.Result;

namespace Application.Product.UseCases.Interfaces;

public interface IDeleteProductUseCase
{
    Task<Result<bool>> ExecuteAsync(long id);
}
