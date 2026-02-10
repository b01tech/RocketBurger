using Core.Shared.Result;
using Core.Shared.Error;

namespace Core.Entities;

public class Category
{
    public long Id { get; init; }
    public string Name { get; private set; }
    public string Description { get; private set; }

    private readonly List<Product> _products = [];
    public IReadOnlyCollection<Product> Products => _products.AsReadOnly();

    private const uint MinNameLength = 3;

    protected Category()
    {
    }

    private Category(string name, string description)
    {
        Name = name;
        Description = description;
    }

    public static Result<Category> Create(string name, string? description)
    {
        if (string.IsNullOrEmpty(name))
            return Result<Category>.Failure(ErrorMessages.NAME_EMPTY);
        if (name.Length < MinNameLength)
            return Result<Category>.Failure(ErrorMessages.NAME_TOOSHORT);

        return new Category(name: name, description: description ?? string.Empty);
    }

    public void Update(string? description) => Description = description ?? string.Empty;
}
