using Application.Category.UseCases;
using Core.Repositories;
using Moq;
using TestUtilities.Builder;

namespace UseCase.Test.Category;

public class GetCategoryUseCaseTest
{
    [Fact]
    public async Task ExecuteAsync_ShouldReturnListOfCategories_WhenCategoriesExist()
    {
        // Arrange
        var categories = new List<Core.Entities.Category>
        {
            CategoryBuilder.Build(inputName: "Category 1"),
            CategoryBuilder.Build(inputName: "Category 2"),
            CategoryBuilder.Build(inputName: "Category 3")
        };

        var repositoryMock = new Mock<ICategoryRepository>();
        repositoryMock.Setup(r => r.GetAllCategoriesAsync()).ReturnsAsync(categories);

        var useCase = new GetCategoryUseCase(repositoryMock.Object);

        // Act
        var result = await useCase.ExecuteAsync();

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Data);
        Assert.Equal(3, result.Data.Count());

        var firstCategory = categories.First();
        var firstResult = result.Data.First(x => x.Name == firstCategory.Name);
        Assert.Equal(firstCategory.Description, firstResult.Description);

        repositoryMock.Verify(r => r.GetAllCategoriesAsync(), Times.Once);
    }

    [Fact]
    public async Task ExecuteAsync_ShouldReturnEmptyList_WhenNoCategoriesExist()
    {
        // Arrange
        var categories = new List<Core.Entities.Category>();

        var repositoryMock = new Mock<ICategoryRepository>();
        repositoryMock.Setup(r => r.GetAllCategoriesAsync()).ReturnsAsync(categories);

        var useCase = new GetCategoryUseCase(repositoryMock.Object);

        // Act
        var result = await useCase.ExecuteAsync();

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Data);
        Assert.Empty(result.Data);

        repositoryMock.Verify(r => r.GetAllCategoriesAsync(), Times.Once);
    }
}
