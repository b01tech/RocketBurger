using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infra.Data;

public class RocketBugerDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(RocketBugerDbContext).Assembly);
    }
}
