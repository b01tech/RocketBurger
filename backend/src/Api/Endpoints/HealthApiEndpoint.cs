namespace Api.Endpoints;

public static class HealthApiEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapGet("/health", () => new { status = "Ok" })
            .WithTags("HealthApi")
            .WithName("HealthApi")
            .WithSummary("Api health checker");
    }
}
