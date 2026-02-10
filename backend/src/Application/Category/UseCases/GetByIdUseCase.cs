namespace Application.Category.UseCases;

using Application.Category.Dtos;
using Application.Category.UseCases.Interfaces;
using Core.Repositories;
using Core.Shared.Result;

internal class GetByIdUseCase(ICategoryRepository repository) : IGetByIdUseCase
{
    public async Task<Result<CategoryResponse>> ExecuteAsync(long id)
    {
        var category = await repository.GetByIdAsync(id);
        if (category is null)
            return Result<CategoryResponse>.Failure(Core.Shared.Error.ErrorMessages.CATEGORY_NOT_FOUND);

        return new CategoryResponse(category.Id, category.Name, category.Description);
    }
}
