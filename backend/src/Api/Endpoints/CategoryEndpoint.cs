using Application.Category.Dtos;
using Application.Category.UseCases.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Endpoints;

public static class CategoryEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/categories").WithTags("Category");

        group
            .MapPost(
                "/",
                async ([FromBody] CreateCategoryRequest request, [FromServices] ICreateCategoryUseCase useCase) =>
                {
                    var result = await useCase.ExecuteAsync(request);
                    return result.IsSuccess
                        ? Results.Created(string.Empty, result.Data)
                        : Results.BadRequest(result.Error);
                }
            )
            .WithName("CreateCategory")
            .WithSummary("Creates a new category")
            .Produces<CategoryResponse>(StatusCodes.Status201Created);
    }
}
