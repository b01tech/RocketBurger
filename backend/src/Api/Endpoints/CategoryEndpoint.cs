using Application.Category.Dtos;
using Application.Category.UseCases.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Endpoints;

public static class CategoryEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/categories").WithTags("Category");

        group.MapGet("/", async ([FromServices] IGetCategoryUseCase useCase) =>
        {
            var result = await useCase.ExecuteAsync();
            if (result.IsSuccess)
                return Results.Ok(result.Data);

            return Results.BadRequest(result.Error!.Errors);
        })
        .WithName("GetCategories")
        .WithSummary("Busca todas as categorias")
        .Produces<IEnumerable<CategoryResponse>>(StatusCodes.Status200OK);

        group.MapPost("/", async ([FromBody] CreateCategoryRequest request, [FromServices] ICreateCategoryUseCase useCase) =>
                {
                    var result = await useCase.ExecuteAsync(request);
                    if (result.IsSuccess)
                        return Results.Created(string.Empty, result.Data);

                    return (result.Error!.StatusCode == StatusCodes.Status409Conflict)
                        ? Results.Conflict(result.Error.Errors)
                        : Results.BadRequest(result.Error.Errors);
                }
            )
            .WithName("CreateCategory")
            .WithSummary("Cria nova categoria")
            .Produces<CategoryResponse>(StatusCodes.Status201Created);

        group.MapPatch("api/categories/{id:long}", async ([FromRoute] long id, [FromBody] UpdateCategoryRequest request,
                [FromServices] IUpdateCategoryUseCase useCase) =>
            {
                var result = await useCase.ExecuteAsync(id, request);
                if (result.IsSuccess)
                    return Results.Ok(result.Data);

                return Results.BadRequest(result.Error!.Errors);
            })
            .WithName("UpdateCategory")
            .WithSummary("Atualiza categoria")
            .Produces<CategoryResponse>(StatusCodes.Status200OK);
    }
}
