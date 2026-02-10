using Core.Shared.Result;

namespace Core.Shared.ValueObjects;

public record Stock
{
    public int Quantity { get; }

    private Stock(int quantity) => Quantity = quantity;

    public static Result<Stock> Create(int quantity)
    {
        if (quantity < 0)
            return Result<Stock>.Failure("Estoque não pode ser negativo");

        return new Stock(quantity);
    }

    public bool IsAvailable(int quantity) => Quantity >= quantity;
    public Result<Stock> Add(int quantity) => Create(Quantity + quantity);

    public Result<Stock> Subtract(int quantity)
    {
        return Quantity < quantity
            ? Result<Stock>.Failure("Estoque insuficiente")
            : Create(Quantity - quantity);
    }


    public static implicit operator int(Stock stock) => stock.Quantity;
}
