using Core.Shared.Result;

namespace Application.Product.UseCases.Interfaces;

public interface IActivateProductUseCase
{
    Task<Result<bool>> ExecuteAsync(long id);
}
