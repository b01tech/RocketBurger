using Application.Category.Dtos;
using Core.Shared.Result;

namespace Application.Category.UseCases.Interfaces;

public interface IUpdateCategoryUseCase
{
    Task<Result<CategoryResponse>> ExecuteAsync(long id, UpdateCategoryRequest request);
}
