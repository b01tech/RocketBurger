using Application.Category.UseCases;
using Core.Repositories;
using Core.Shared.Error;
using Moq;
using TestUtilities.Builder;

namespace UseCase.Test.Category;

public class CreateCategoryUseCaseTest
{
    [Fact]
    public async Task ExecuteAsync_ShouldReturnSuccess_WhenRequestIsValid()
    {
        // Arrange
        var request = CreateCategoryRequestBuilder.Build();
        var repositoryMock = CreateCategoryRepositoryMock();
        var unitOfWorkMock = new Mock<IUnitOfWork>();

        var useCase = new CreateCategoryUseCase(repositoryMock.Object, unitOfWorkMock.Object);

        // Act
        var result = await useCase.ExecuteAsync(request);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Data);
        Assert.Equal(request.Name, result.Data.Name);
        Assert.Equal(request.Description, result.Data.Description);

        repositoryMock.Verify(r => r.AddCategoryAsync(It.IsAny<Core.Entities.Category>()), Times.Once);
        unitOfWorkMock.Verify(u => u.CommitAsync(), Times.Once);
    }

    [Fact]
    public async Task ExecuteAsync_ShouldReturnFailure_WhenCategoryAlreadyExists()
    {
        // Arrange
        var request = CreateCategoryRequestBuilder.Build();
        var repositoryMock = CreateCategoryRepositoryMock();
        var unitOfWorkMock = new Mock<IUnitOfWork>();

        repositoryMock.Setup(r => r.CategoryExistsAsync(request.Name)).ReturnsAsync(true);

        var useCase = new CreateCategoryUseCase(repositoryMock.Object, unitOfWorkMock.Object);

        // Act
        var result = await useCase.ExecuteAsync(request);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(ErrorMessages.CATEGORY_ALREADY_EXISTS, result.Error!.Errors.First());

        repositoryMock.Verify(r => r.AddCategoryAsync(It.IsAny<Core.Entities.Category>()), Times.Never);
        unitOfWorkMock.Verify(u => u.CommitAsync(), Times.Never);
    }

    [Fact]
    public async Task ExecuteAsync_ShouldReturnFailure_WhenNameIsInvalid()
    {
        // Arrange
        var request = CreateCategoryRequestBuilder.Build(inputName: "");
        var repositoryMock = CreateCategoryRepositoryMock();
        var unitOfWorkMock = new Mock<IUnitOfWork>();

        var useCase = new CreateCategoryUseCase(repositoryMock.Object, unitOfWorkMock.Object);

        // Act
        var result = await useCase.ExecuteAsync(request);

        // Assert
        Assert.False(result.IsSuccess);
        repositoryMock.Verify(r => r.CategoryExistsAsync(It.IsAny<string>()), Times.Never);
        repositoryMock.Verify(r => r.AddCategoryAsync(It.IsAny<Core.Entities.Category>()), Times.Never);
        unitOfWorkMock.Verify(u => u.CommitAsync(), Times.Never);
    }


    private static Mock<ICategoryRepository> CreateCategoryRepositoryMock()
    {
        var mock = new Mock<ICategoryRepository>();
        mock.Setup(r => r.CategoryExistsAsync(It.IsAny<string>())).ReturnsAsync(false);
        mock.Setup(r => r.AddCategoryAsync(It.IsAny<Core.Entities.Category>()))
            .ReturnsAsync((Core.Entities.Category c) => c);
        return mock;
    }
}
