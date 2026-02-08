using Application.Category.Dtos;
using Application.Category.UseCases.Interfaces;
using Core.Shared.Result;

namespace Application.Category.UseCases;

internal class CreateCategoryUseCase : ICreateCategoryUseCase
{
    public Task<Result<CategoryResponse>> ExecuteAsync(CreateCategoryRequest request)
    {
        throw new NotImplementedException();
    }
}
