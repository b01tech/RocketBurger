using Application.Category.UseCases;
using Core.Repositories;
using Core.Shared.Error;
using Moq;
using TestUtilities.Builder;

namespace UseCase.Test.Category;

public class GetByIdUseCaseTest
{
    [Fact]
    public async Task ExecuteAsync_ShouldReturnCategory_WhenCategoryExists()
    {
        // Arrange
        var category = CategoryBuilder.Build();
        var repositoryMock = new Mock<ICategoryRepository>();
        long id = 1;
        repositoryMock.Setup(r => r.GetByIdAsync(id)).ReturnsAsync(category);

        var useCase = new GetByIdUseCase(repositoryMock.Object);

        // Act
        var result = await useCase.ExecuteAsync(id);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Data);
        Assert.Equal(category.Name, result.Data.Name);
        Assert.Equal(category.Description, result.Data.Description);

        repositoryMock.Verify(r => r.GetByIdAsync(id), Times.Once);
    }

    [Fact]
    public async Task ExecuteAsync_ShouldReturnFailure_WhenCategoryDoesNotExist()
    {
        // Arrange
        long id = 99;
        var repositoryMock = new Mock<ICategoryRepository>();
        repositoryMock.Setup(r => r.GetByIdAsync(id)).ReturnsAsync((Core.Entities.Category?)null);

        var useCase = new GetByIdUseCase(repositoryMock.Object);

        // Act
        var result = await useCase.ExecuteAsync(id);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(ErrorMessages.CATEGORY_NOT_FOUND, result.Error!.Errors.First());

        repositoryMock.Verify(r => r.GetByIdAsync(id), Times.Once);
    }
}
