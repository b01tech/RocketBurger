using Core.Entities;
using Core.Shared.ValueObjects;
using Core.Shared.Error;

namespace Core.Test.Entities;

public class ProductTest
{
    [Fact]
    public void Create_ShouldReturnSuccess_WhenDataIsValid()
    {
        // Arrange
        var name = "X-Bacon";
        var description = "Delicioso X-Bacon";
        var imageUrl = "http://img.com/xbacon.jpg";
        var price = Price.Create(25.00m).Data!;
        var categoryId = 1;

        // Act
        var result = Product.Create(name, description, imageUrl, price, categoryId);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Data);
        Assert.Equal(name, result.Data.Name);
        Assert.Equal(description, result.Data.Description);
        Assert.Equal(imageUrl, result.Data.ImageUrl);
        Assert.Equal(price, result.Data.Price);
        Assert.Equal(categoryId, result.Data.CategoryId);
        Assert.Equal(0, result.Data.Stock.Quantity); // Default stock is 0
        Assert.True(result.Data.IsActive); // Default IsActive is true
    }

    [Fact]
    public void Create_ShouldReturnFailure_WhenNameIsInvalid()
    {
        // Arrange
        var price = Price.Create(25.00m).Data!;

        // Act
        var result = Product.Create("", "desc", "url", price, 1);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Contains(ErrorMessages.NAME_EMPTY, result.Error!.Errors);
    }

    [Fact]
    public void Update_ShouldUpdateProductDetails()
    {
        // Arrange
        var product = Product.Create("Old Name", "Old Desc", "Old Url", Price.Create(10).Data!, 1).Data!;
        var newName = "New Name";
        var newDesc = "New Desc";
        var newUrl = "New Url";
        var newCategoryId = 2;

        // Act
        var result = product.Update(newName, newDesc, newUrl, newCategoryId);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(newName, product.Name);
        Assert.Equal(newDesc, product.Description);
        Assert.Equal(newUrl, product.ImageUrl);
        Assert.Equal(newCategoryId, product.CategoryId);
    }

    [Fact]
    public void UpdatePrice_ShouldUpdatePrice()
    {
        // Arrange
        var product = Product.Create("Name", "Desc", "Url", Price.Create(10).Data!, 1).Data!;
        var newPriceValue = 20.00m;

        // Act
        var result = product.UpdatePrice(newPriceValue);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(newPriceValue, product.Price.Value);
    }

    [Fact]
    public void AddStock_ShouldIncreaseStock()
    {
        // Arrange
        var product = Product.Create("Name", "Desc", "Url", Price.Create(10).Data!, 1).Data!;
        var quantityToAdd = 10;

        // Act
        var result = product.AddStock(quantityToAdd);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(10, product.Stock.Quantity);
    }

    [Fact]
    public void RemoveStock_ShouldDecreaseStock_WhenSufficient()
    {
        // Arrange
        var product = Product.Create("Name", "Desc", "Url", Price.Create(10).Data!, 1).Data!;
        product.AddStock(10); // Start with 10

        // Act
        var result = product.RemoveStock(5);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(5, product.Stock.Quantity);
    }

    [Fact]
    public void RemoveStock_ShouldReturnFailure_WhenInsufficient()
    {
        // Arrange
        var product = Product.Create("Name", "Desc", "Url", Price.Create(10).Data!, 1).Data!;
        product.AddStock(5); // Start with 5

        // Act
        var result = product.RemoveStock(10);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(5, product.Stock.Quantity); // Should remain same
        Assert.Contains("Estoque insuficiente", result.Error!.Errors);
    }

    [Fact]
    public void Activate_ShouldSetIsActiveToTrue()
    {
        // Arrange
        var product = Product.Create("Name", "Desc", "Url", Price.Create(10).Data!, 1).Data!;
        product.Deactivate(); // First ensure it is false

        // Act
        product.Activate();

        // Assert
        Assert.True(product.IsActive);
    }

    [Fact]
    public void Deactivate_ShouldSetIsActiveToFalse()
    {
        // Arrange
        var product = Product.Create("Name", "Desc", "Url", Price.Create(10).Data!, 1).Data!;

        // Act
        product.Deactivate();

        // Assert
        Assert.False(product.IsActive);
    }
}
