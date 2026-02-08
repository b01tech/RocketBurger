namespace Core.Repositories;

public interface IUnitOfWork
{
    Task CommitAsync();
}
