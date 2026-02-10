using System.Net;
using Application.Product.UseCases.Interfaces;
using Core.Repositories;
using Core.Shared.Result;

namespace Application.Product.UseCases;

internal class ActivateProductUseCase(IProductRepository repository, IUnitOfWork unitOfWork) : IActivateProductUseCase
{
    public async Task<Result<bool>> ExecuteAsync(long id)
    {
        var success = await repository.ActivateAsync(id);
        if (!success)
            return Result<bool>.Failure(Core.Shared.Error.ErrorMessages.PRODUCT_NOT_FOUND, (int)HttpStatusCode.NotFound);

        await unitOfWork.CommitAsync();
        return true;
    }
}
