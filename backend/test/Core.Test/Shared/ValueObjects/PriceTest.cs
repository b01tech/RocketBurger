using Core.Shared.ValueObjects;

namespace Core.Test.Shared.ValueObjects;

public class PriceTest
{
    [Fact]
    public void Create_ShouldReturnSuccess_WhenValueIsValid()
    {
        // Arrange
        decimal value = 10.50m;

        // Act
        var result = Price.Create(value);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(value, result.Data!.Value);
    }

    [Fact]
    public void Create_ShouldReturnFailure_WhenValueIsNegative()
    {
        // Arrange
        decimal value = -1.00m;

        // Act
        var result = Price.Create(value);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Contains("O preço não pode ser negativo", result.Error!.Errors);
    }

    [Fact]
    public void ImplicitOperator_ShouldReturnDecimalValue()
    {
        // Arrange
        decimal value = 15.00m;
        var price = Price.Create(value).Data!;

        // Act
        decimal result = price;

        // Assert
        Assert.Equal(value, result);
    }

    [Fact]
    public void Equality_ShouldBeEqual_WhenValuesAreSame()
    {
        // Arrange
        var price1 = Price.Create(10.00m).Data!;
        var price2 = Price.Create(10.00m).Data!;

        // Act & Assert
        Assert.Equal(price1, price2);
        Assert.True(price1 == price2);
    }

    [Fact]
    public void ApplyDiscount_ShouldReducePriceCorrectly()
    {
        // Arrange
        var price = Price.Create(100.00m).Data!;
        var discountPercentage = 20.00m;

        // Act
        var result = price.ApplyDiscount(discountPercentage);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(80.00m, result.Data!.Value);
    }

    [Fact]
    public void ApplyDiscount_ShouldReturnFailure_WhenPercentageIsInvalid()
    {
        // Arrange
        var price = Price.Create(100.00m).Data!;

        // Act
        var resultNegative = price.ApplyDiscount(-5);
        var resultTooHigh = price.ApplyDiscount(105);

        // Assert
        Assert.False(resultNegative.IsSuccess);
        Assert.Contains("A porcentagem de desconto deve estar entre 0 e 100", resultNegative.Error!.Errors);

        Assert.False(resultTooHigh.IsSuccess);
        Assert.Contains("A porcentagem de desconto deve estar entre 0 e 100", resultTooHigh.Error!.Errors);
    }

    [Fact]
    public void Add_ShouldSumPricesCorrectly()
    {
        // Arrange
        var price1 = Price.Create(10.00m).Data!;
        var price2 = Price.Create(20.00m).Data!;

        // Act
        var result = price1.Add(price2);

        // Assert
        Assert.Equal(30.00m, result.Value);
    }
}
