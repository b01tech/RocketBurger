using Application.Category.Dtos;
using Application.Category.UseCases.Interfaces;
using Core.Repositories;
using Core.Shared.Result;

namespace Application.Category.UseCases;

public class GetCategoryUseCase(ICategoryRepository repository) : IGetCategoryUseCase
{
    public async Task<Result<IEnumerable<CategoryResponse>>> ExecuteAsync()
    {
        var categories = await repository.GetAllCategoriesAsync();
        return Result<IEnumerable<CategoryResponse>>.Success(categories.Select(x => new CategoryResponse(x.Id, x.Name, x.Description)));
    }
}
