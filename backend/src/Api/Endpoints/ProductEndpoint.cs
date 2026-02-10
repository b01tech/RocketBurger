using System.Net;
using Application.Product.Dtos;
using Application.Product.UseCases.Interfaces;
using Application.Shared;
using Core.Shared.Result;
using Microsoft.AspNetCore.Mvc;

namespace Api.Endpoints;

public static class ProductEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/products").WithTags("Product");

        group.MapPost("/",
                async ([FromBody] CreateProductRequest request, [FromServices] ICreateProductUseCase useCase) =>
                {
                    var result = await useCase.ExecuteAsync(request);
                    if (result.IsSuccess)
                        return Results.Created($"/api/products/{result.Data!.Id}", result.Data);

                    return Results.BadRequest(result.Error);
                })
            .WithName("CreateProduct")
            .WithSummary("Cria um novo produto")
            .Produces<ProductResponse>(StatusCodes.Status201Created)
            .Produces<ErrorResult>(StatusCodes.Status400BadRequest);

        group.MapPut("/{id:long}",
                async (long id, [FromBody] UpdateProductRequest request,
                    [FromServices] IUpdateProductUseCase useCase) =>
                {
                    if (id != request.Id)
                        return Results.BadRequest(new ErrorResult(["ID da rota e do corpo da requisição não conferem"],
                            (int)HttpStatusCode.BadRequest));

                    var result = await useCase.ExecuteAsync(request);
                    if (result.IsSuccess)
                        return Results.Ok(result.Data);

                    return result.Error?.StatusCode switch
                    {
                        (int)HttpStatusCode.NotFound => Results.NotFound(result.Error),
                        _ => Results.BadRequest(result.Error)
                    };
                })
            .WithName("UpdateProduct")
            .WithSummary("Atualiza um produto existente")
            .Produces<ProductResponse>(StatusCodes.Status200OK)
            .Produces<ErrorResult>(StatusCodes.Status400BadRequest)
            .Produces<ErrorResult>(StatusCodes.Status404NotFound);

        group.MapDelete("/{id:long}", async (long id, [FromServices] IDeleteProductUseCase useCase) =>
            {
                var result = await useCase.ExecuteAsync(id);
                if (result.IsSuccess)
                    return Results.NoContent();

                return Results.NotFound(result.Error);
            })
            .WithName("DeleteProduct")
            .WithSummary("Remove um produto")
            .Produces(StatusCodes.Status204NoContent)
            .Produces<ErrorResult>(StatusCodes.Status404NotFound);

        group.MapPatch("/activate/{id:long}", async (long id, [FromServices] IActivateProductUseCase useCase) =>
            {
                var result = await useCase.ExecuteAsync(id);
                if (result.IsSuccess)
                    return Results.NoContent();

                return Results.NotFound(result.Error);
            })
            .WithName("ActivateProduct")
            .WithSummary("Ativa um produto")
            .Produces(StatusCodes.Status204NoContent)
            .Produces<ErrorResult>(StatusCodes.Status404NotFound);

        group.MapGet("/{id:long}", async (long id, [FromServices] IGetProductByIdUseCase useCase) =>
            {
                var result = await useCase.ExecuteAsync(id);
                if (result.IsSuccess)
                    return Results.Ok(result.Data);

                return Results.NotFound(result.Error);
            })
            .WithName("GetProductById")
            .WithSummary("Busca um produto por ID")
            .Produces<ProductResponse>(StatusCodes.Status200OK)
            .Produces<ErrorResult>(StatusCodes.Status404NotFound);

        group.MapGet("/",
                async ([FromServices] IGetProductsUseCase useCase, [FromQuery] int page = 1, int pageSize = 25) =>
                {
                    var result = await useCase.ExecuteAsync(page, pageSize);
                    if (result.IsSuccess)
                        return Results.Ok(result.Data);

                    return Results.BadRequest(result.Error);
                })
            .WithName("GetProducts")
            .WithSummary("Lista produtos ativos")
            .Produces<PaginationResponse<ProductResponse>>(StatusCodes.Status200OK)
            .Produces<ErrorResult>(StatusCodes.Status400BadRequest);
    }
}
