using Core.Repositories;
using Infra.Data;

namespace Infra.Repositories;

internal class UnitOfWork(RocketBugerDbContext dbContext) : IUnitOfWork
{
    public async Task CommitAsync() => await dbContext.SaveChangesAsync();
}
