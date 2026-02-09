using Application.Category.UseCases;
using Core.Repositories;
using Moq;
using TestUtilities.Builder;

namespace UseCase.Test.Category;

public class UpdateCategoryUseCaseTest
{
    [Fact]
    public async Task ExecuteAsync_ShouldReturnSuccess_WhenCategoryExists()
    {
        // Arrange
        var category = CategoryBuilder.Build();
        var request = UpdateCategoryRequestBuilder.Build();

        var repositoryMock = CreateCategoryRepositoryMock(category);
        var unitOfWorkMock = new Mock<IUnitOfWork>();


        var useCase = new UpdateCategoryUseCase(repositoryMock.Object, unitOfWorkMock.Object);

        // Act
        var result = await useCase.ExecuteAsync(category.Id, request);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Data);
        Assert.Equal(category.Name, result.Data.Name);
        Assert.Equal(request.Description, result.Data.Description);

        unitOfWorkMock.Verify(u => u.CommitAsync(), Times.Once);
    }

    [Fact]
    public async Task ExecuteAsync_ShouldReturnFailure_WhenCategoryDoesNotExist()
    {
        // Arrange
        const long id = 123L;
        var request = UpdateCategoryRequestBuilder.Build();

        var repositoryMock = CreateCategoryRepositoryMock(null);
        var unitOfWorkMock = new Mock<IUnitOfWork>();

        var useCase = new UpdateCategoryUseCase(repositoryMock.Object, unitOfWorkMock.Object);

        // Act
        var result = await useCase.ExecuteAsync(id, request);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal("Categoria não encontrada", result.Error!.Errors.First());

        unitOfWorkMock.Verify(u => u.CommitAsync(), Times.Never);
    }

    private static Mock<ICategoryRepository> CreateCategoryRepositoryMock(Core.Entities.Category? category = null)
    {
        var mock = new Mock<ICategoryRepository>();
        if (category is null)
            mock.Setup(r => r.GetCategoryByIdAsync(1)).ReturnsAsync((Core.Entities.Category?)null);
        else
            mock.Setup(r => r.GetCategoryByIdAsync(category.Id)).ReturnsAsync(category);

        return mock;
    }
}
