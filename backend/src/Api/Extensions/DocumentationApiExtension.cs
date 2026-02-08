using Scalar.AspNetCore;

namespace Api.Extensions;

public static class DocumentationApiExtension
{
    public static IServiceCollection AddDocumentationApi(this IServiceCollection services)
    {
        services.AddOpenApi();
        return services;
    }

    public static void UseDocumentarionApi(this IEndpointRouteBuilder app)
    {
        app.MapOpenApi();
        app.MapScalarApiReference("/docs", options =>
        {
            options.Title = "Rocket Burger API";
            options.Theme = ScalarTheme.Solarized;
        });

    }
}
