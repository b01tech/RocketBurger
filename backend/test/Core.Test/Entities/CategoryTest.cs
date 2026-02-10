using Core.Entities;
using Core.Shared.Error;

namespace Core.Test.Entities;

public class CategoryTest
{
    [Fact]
    public void Create_ShouldReturnSuccess_WhenDataIsValid()
    {
        // Arrange
        var name = "Valid Name";
        var description = "Valid Description";

        // Act
        var result = Category.Create(name, description);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Data);
        Assert.Equal(name, result.Data.Name);
        Assert.Equal(description, result.Data.Description);
        Assert.NotNull(result.Data.Products);
        Assert.Empty(result.Data.Products);
    }

    [Fact]
    public void Create_ShouldReturnSuccess_WhenDescriptionIsNull()
    {
        // Arrange
        var name = "Valid Name";

        // Act
        var result = Category.Create(name, null);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Data);
        Assert.Equal(name, result.Data.Name);
        Assert.Equal(string.Empty, result.Data.Description);
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void Create_ShouldReturnFailure_WhenNameIsNullOrEmpty(string? name)
    {
        // Arrange & Act
        var result = Category.Create(name!, "description");

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Contains(ErrorMessages.NAME_EMPTY, result.Error!.Errors);
    }

    [Fact]
    public void Create_ShouldReturnFailure_WhenNameIsTooShort()
    {
        // Arrange
        var name = "Ab";

        // Act
        var result = Category.Create(name, "description");

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Contains(ErrorMessages.NAME_TOOSHORT, result.Error!.Errors);
    }

    [Fact]
    public void Update_ShouldUpdateDescription()
    {
        // Arrange
        var category = Category.Create("Valid Name", "Old Description").Data!;
        var newDescription = "New Description";

        // Act
        category.Update(newDescription);

        // Assert
        Assert.Equal(newDescription, category.Description);
        Assert.NotEqual("Old Description", category.Description);
    }

    [Fact]
    public void Update_ShouldSetDescriptionToEmpty_WhenValueIsNull()
    {
        // Arrange
        var category = Category.Create("Lanches", "Antiga descrição").Data!;

        // Act
        category.Update(null);

        // Assert
        Assert.Equal(string.Empty, category.Description);
    }
}
