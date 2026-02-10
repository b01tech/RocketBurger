using Application.Category.Dtos;
using Core.Shared.Result;

namespace Application.Category.UseCases.Interfaces;

public interface IGetByIdUseCase
{
    Task<Result<CategoryResponse>> ExecuteAsync(long id);
}
