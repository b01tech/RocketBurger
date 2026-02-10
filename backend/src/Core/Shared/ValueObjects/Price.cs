using Core.Shared.Result;

namespace Core.Shared.ValueObjects;

public record Price
{
    public decimal Value { get; }

    private Price(decimal value)
    {
        Value = value;
    }

    public static Result<Price> Create(decimal value)
    {
        if (value < 0)
            return Result<Price>.Failure(Error.ErrorMessages.PRICE_NEGATIVE);

        return new Price(value);
    }

    public Result<Price> ApplyDiscount(decimal percentage)
    {
        if (percentage is < 0 or > 100)
            return Result<Price>.Failure(Error.ErrorMessages.DISCOUNT_INVALID);

        var discount = Value * (percentage / 100);
        return new Price(Value - discount);
    }

    public Price Add(Price other) => new(Value + other.Value);

    public static implicit operator decimal(Price price) => price.Value;
}
