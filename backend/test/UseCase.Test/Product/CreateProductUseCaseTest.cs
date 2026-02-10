using Application.Product.UseCases;
using Core.Entities;
using Core.Repositories;
using Moq;
using TestUtilities.Builder;
using Xunit;

namespace UseCase.Test.Product;

public class CreateProductUseCaseTest
{
    [Fact]
    public async Task ExecuteAsync_ShouldReturnSuccess_WhenRequestIsValid()
    {
        // Arrange
        var request = CreateProductRequestBuilder.Build();
        var category = CategoryBuilder.Build();

        // Ensure category ID matches
        var categoryProp = category.GetType().GetProperty("Id");
        categoryProp?.SetValue(category, request.CategoryId);

        var productRepositoryMock = new Mock<IProductRepository>();
        var categoryRepositoryMock = new Mock<ICategoryRepository>();
        var unitOfWorkMock = new Mock<IUnitOfWork>();

        categoryRepositoryMock.Setup(r => r.GetCategoryByIdAsync(request.CategoryId))
            .ReturnsAsync(category);

        productRepositoryMock.Setup(r => r.AddAsync(It.IsAny<Core.Entities.Product>()))
            .ReturnsAsync((Core.Entities.Product p) => p);

        var useCase = new CreateProductUseCase(productRepositoryMock.Object, categoryRepositoryMock.Object, unitOfWorkMock.Object);

        // Act
        var result = await useCase.ExecuteAsync(request);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Data);
        Assert.Equal(request.Name, result.Data.Name);

        productRepositoryMock.Verify(r => r.AddAsync(It.IsAny<Core.Entities.Product>()), Times.Once);
        unitOfWorkMock.Verify(u => u.CommitAsync(), Times.Once);
    }

    [Fact]
    public async Task ExecuteAsync_ShouldReturnFailure_WhenCategoryNotFound()
    {
        // Arrange
        var request = CreateProductRequestBuilder.Build();

        var productRepositoryMock = new Mock<IProductRepository>();
        var categoryRepositoryMock = new Mock<ICategoryRepository>();
        var unitOfWorkMock = new Mock<IUnitOfWork>();

        categoryRepositoryMock.Setup(r => r.GetCategoryByIdAsync(request.CategoryId))
            .ReturnsAsync((Core.Entities.Category?)null);

        var useCase = new CreateProductUseCase(productRepositoryMock.Object, categoryRepositoryMock.Object, unitOfWorkMock.Object);

        // Act
        var result = await useCase.ExecuteAsync(request);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal("Categoria não encontrada", result.Error!.Errors.First());

        productRepositoryMock.Verify(r => r.AddAsync(It.IsAny<Core.Entities.Product>()), Times.Never);
        unitOfWorkMock.Verify(u => u.CommitAsync(), Times.Never);
    }

    [Fact]
    public async Task ExecuteAsync_ShouldReturnFailure_WhenPriceIsInvalid()
    {
        // Arrange
        var baseRequest = CreateProductRequestBuilder.Build();
        var request = new Application.Product.Dtos.CreateProductRequest(
            baseRequest.Name,
            baseRequest.Description,
            -10m, // Invalid price
            baseRequest.ImageUrl,
            baseRequest.CategoryId,
            baseRequest.StockQuantity
        );

        var category = CategoryBuilder.Build();

        var productRepositoryMock = new Mock<IProductRepository>();
        var categoryRepositoryMock = new Mock<ICategoryRepository>();
        var unitOfWorkMock = new Mock<IUnitOfWork>();

        categoryRepositoryMock.Setup(r => r.GetCategoryByIdAsync(request.CategoryId))
            .ReturnsAsync(category);

        var useCase = new CreateProductUseCase(productRepositoryMock.Object, categoryRepositoryMock.Object, unitOfWorkMock.Object);

        // Act
        var result = await useCase.ExecuteAsync(request);

        // Assert
        Assert.False(result.IsSuccess);

        productRepositoryMock.Verify(r => r.AddAsync(It.IsAny<Core.Entities.Product>()), Times.Never);
        unitOfWorkMock.Verify(u => u.CommitAsync(), Times.Never);
    }
}
