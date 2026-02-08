using Infra.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infra.Extensions;

public static class InfrastructureExtension
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        AddAppDbContext(services, configuration);
        return services;
    }

    private static void AddAppDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DbConnection") ?? string.Empty;

        services.AddDbContext<RocketBugerDbContext>(options =>
            options.UseSqlite(connectionString));
    }
}
