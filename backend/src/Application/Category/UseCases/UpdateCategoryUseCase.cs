using Application.Category.Dtos;
using Application.Category.UseCases.Interfaces;
using Core.Repositories;
using Core.Shared.Result;

namespace Application.Category.UseCases;

internal class UpdateCategoryUseCase(ICategoryRepository repository, IUnitOfWork unitOfWork) : IUpdateCategoryUseCase
{
    public async Task<Result<CategoryResponse>> ExecuteAsync(long id, UpdateCategoryRequest request)
    {
        var category = await repository.GetCategoryByIdAsync(id);
        if (category is null)
            return Result<CategoryResponse>.Failure("Categoria não encontrada");

        category.Update(request.Description);
        await unitOfWork.CommitAsync();
        return new CategoryResponse(category.Id, category.Name, category.Description);
    }
}
