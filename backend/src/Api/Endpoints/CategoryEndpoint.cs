using Application.Category.Dtos;
using Application.Category.UseCases.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Endpoints;

public static class CategoryEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapGroup("api/categories").WithTags("Category").MapOpenApi();

        app.MapPost(
                "/",
                async ([FromBody] CreateCategoryRequest request, [FromServices] ICreateCategoryUseCase useCase) =>
                {
                    var result = await useCase.ExecuteAsync(request);
                    return result.IsSuccess ? Results.Ok(result.Data) : Results.BadRequest(result.Error);
                }
            )
            .WithName("CreateCategory")
            .WithSummary("Creates a new category");
    }
}
