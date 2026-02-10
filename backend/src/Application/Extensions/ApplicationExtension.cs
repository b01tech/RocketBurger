using Application.Category.UseCases;
using Application.Category.UseCases.Interfaces;
using Application.Product.UseCases;
using Application.Product.UseCases.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Extensions;

public static class ApplicationExtension
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        AddUseCases(services);
        return services;
    }

    private static void AddUseCases(IServiceCollection services)
    {
        services.AddScoped<ICreateCategoryUseCase, CreateCategoryUseCase>();
        services.AddScoped<IUpdateCategoryUseCase, UpdateCategoryUseCase>();

        services.AddScoped<IActivateProductUseCase, ActivateProductUseCase>();
        services.AddScoped<ICreateProductUseCase, CreateProductUseCase>();
        services.AddScoped<IDeleteProductUseCase, DeleteProductUseCase>();
        services.AddScoped<IGetProductByIdUseCase, GetProductByIdUseCase>();
        services.AddScoped<IGetProductsUseCase, GetProductsUseCase>();
        services.AddScoped<IUpdateProductUseCase, UpdateProductUseCase>();
    }
}
