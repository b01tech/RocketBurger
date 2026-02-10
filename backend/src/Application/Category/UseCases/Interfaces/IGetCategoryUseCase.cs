using Application.Category.Dtos;
using Core.Shared.Result;

namespace Application.Category.UseCases.Interfaces;

public interface IGetCategoryUseCase
{
    Task<Result<IEnumerable<CategoryResponse>>> ExecuteAsync();
}
