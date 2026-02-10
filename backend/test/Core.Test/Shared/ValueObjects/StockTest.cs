using Core.Shared.ValueObjects;
using Core.Shared.Error;

namespace Core.Test.Shared.ValueObjects;

public class StockTest
{
    [Fact]
    public void Create_ShouldReturnSuccess_WhenQuantityIsValid()
    {
        // Arrange
        int quantity = 10;

        // Act
        var result = Stock.Create(quantity);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(quantity, result.Data!.Quantity);
    }

    [Fact]
    public void Create_ShouldReturnFailure_WhenQuantityIsNegative()
    {
        // Arrange
        int quantity = -5;

        // Act
        var result = Stock.Create(quantity);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Contains(ErrorMessages.STOCK_NEGATIVE, result.Error!.Errors);
    }

    [Fact]
    public void ImplicitOperator_ShouldReturnIntValue()
    {
        // Arrange
        int quantity = 20;
        var stock = Stock.Create(quantity).Data!;

        // Act
        int result = stock;

        // Assert
        Assert.Equal(quantity, result);
    }

    [Fact]
    public void Equality_ShouldBeEqual_WhenQuantitiesAreSame()
    {
        // Arrange
        var stock1 = Stock.Create(10).Data!;
        var stock2 = Stock.Create(10).Data!;

        // Act & Assert
        Assert.Equal(stock1, stock2);
        Assert.True(stock1 == stock2);
    }

    [Theory]
    [InlineData(10, 5, true)]
    [InlineData(10, 10, true)]
    [InlineData(10, 15, false)]
    public void IsAvailable_ShouldReturnExpectedResult(int initialQuantity, int requestedQuantity, bool expected)
    {
        // Arrange
        var stock = Stock.Create(initialQuantity).Data!;

        // Act
        var result = stock.IsAvailable(requestedQuantity);

        // Assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public void Add_ShouldIncreaseStockCorrectly()
    {
        // Arrange
        var stock = Stock.Create(10).Data!;
        var quantityToAdd = 5;

        // Act
        var result = stock.Add(quantityToAdd);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(15, result.Data!.Quantity);
    }

    [Fact]
    public void Subtract_ShouldDecreaseStockCorrectly_WhenStockIsSufficient()
    {
        // Arrange
        var stock = Stock.Create(10).Data!;
        var quantityToSubtract = 5;

        // Act
        var result = stock.Subtract(quantityToSubtract);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(5, result.Data!.Quantity);
    }

    [Fact]
    public void Subtract_ShouldReturnFailure_WhenStockIsInsufficient()
    {
        // Arrange
        var stock = Stock.Create(10).Data!;
        var quantityToSubtract = 15;

        // Act
        var result = stock.Subtract(quantityToSubtract);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Contains("Estoque insuficiente", result.Error!.Errors);
    }
}
