using System.Net;
using Application.Category.Dtos;
using Application.Category.UseCases.Interfaces;
using Core.Repositories;
using Core.Shared.Result;

namespace Application.Category.UseCases;

internal class CreateCategoryUseCase(ICategoryRepository repository, IUnitOfWork unitOfWork) : ICreateCategoryUseCase
{
    public async Task<Result<CategoryResponse>> ExecuteAsync(CreateCategoryRequest request)
    {
        var resultCategory = Core.Entities.Category.Create(request.Name, request.Description);
        if (!resultCategory.IsSuccess)
            return Result<CategoryResponse>.Failure(resultCategory.Error!.Errors, (int)HttpStatusCode.BadRequest);

        var categoryExists = await repository.CategoryExistsAsync(request.Name);
        if (categoryExists)
            return Result<CategoryResponse>.Failure("Categoria já cadastrada", (int)HttpStatusCode.Conflict);

        var category = await repository.AddCategoryAsync(resultCategory.Data!);
        await unitOfWork.CommitAsync();
        return new CategoryResponse(category.Id, category.Name, category.Description ?? string.Empty);
    }
}
