namespace Api.Extensions;

public static class CorsApiExtension
{
    private const string PolicyName = "AllowFrontend";

    public static IServiceCollection AddCorsApi(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy(PolicyName, builder =>
            {
                builder
                    .WithOrigins("http://localhost:4200", "http://localhost:5173")
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            });
        });

        return services;
    }

    public static WebApplication UseCorsApi(this WebApplication app)
    {
        app.UseCors(PolicyName);
        return app;
    }
}
