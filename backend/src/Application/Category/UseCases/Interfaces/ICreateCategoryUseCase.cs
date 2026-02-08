using Application.Category.Dtos;
using Core.Shared.Result;

namespace Application.Category.UseCases.Interfaces;

public interface ICreateCategoryUseCase
{
    Task<Result<CategoryResponse>> ExecuteAsync(CreateCategoryRequest request);
}
