using Core.Shared.Result;
using Core.Shared.ValueObjects;
using Core.Shared.Error;

namespace Core.Entities;

public class Product
{
    public long Id { get; init; }
    public string Name { get; private set; } = string.Empty;
    public string? Description { get; private set; }
    public string? ImageUrl { get; private set; }
    public Price Price { get; private set; }

    public long CategoryId { get; private set; }
    public Category? Category { get; private set; }

    public Stock Stock { get; private set; } = Stock.Create(0).Data!;

    public bool IsActive { get; private set; } = true;

    private const uint MinNameLength = 3;

    protected Product()
    {
    }

    private Product(string name, string? description, string? imageUrl, Price price, long categoryId,
        Stock? stock = null)
    {
        Name = name;
        Description = description;
        ImageUrl = imageUrl;
        Price = price;
        CategoryId = categoryId;
        if (stock is not null)
            Stock = stock;
    }

    public static Result<Product> Create(string name, string? description, string? imageUrl, Price price,
        long categoryId)
    {
        List<string> errors = [];

        if (string.IsNullOrWhiteSpace(name))
            errors.Add(ErrorMessages.NAME_EMPTY);
        else if (name.Length < MinNameLength)
            errors.Add(ErrorMessages.NAME_TOOSHORT);

        if (errors.Count > 0)
            return Result<Product>.Failure(errors);

        return new Product(name, description, imageUrl, price, categoryId);
    }

    public Result<bool> Update(string name, string? description, string? imageUrl, long categoryId)
    {
        List<string> errors = [];
        if (string.IsNullOrWhiteSpace(name))
            errors.Add(ErrorMessages.NAME_EMPTY);

        if (errors.Count > 0)
            return Result<bool>.Failure(errors);

        Name = name;
        Description = description;
        ImageUrl = imageUrl;
        CategoryId = categoryId;
        return true;
    }

    public Result<bool> UpdatePrice(decimal newPrice)
    {
        Price = Price.Create(newPrice).Data!;
        return true;
    }

    public Result<bool> AddStock(int quantity)
    {
        var result = Stock.Add(quantity);
        if (!result.IsSuccess)
            return Result<bool>.Failure(result.Error!.Errors);

        Stock = result.Data!;
        return true;
    }

    public Result<bool> RemoveStock(int quantity)
    {
        var result = Stock.Subtract(quantity);
        if (!result.IsSuccess)
            return Result<bool>.Failure(result.Error!.Errors);

        Stock = result.Data!;
        return true;
    }

    public void Activate() => IsActive = true;
    public void Deactivate() => IsActive = false;
}
