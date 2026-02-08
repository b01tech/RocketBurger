using Api.Endpoints;

namespace Api.Extensions;

public static class EndpointExtension
{
    public static void MapEndpoints(this IEndpointRouteBuilder app)
    {
        HealthApiEndpoint.Map(app);
        CategoryEndpoint.Map(app);
    }
}
