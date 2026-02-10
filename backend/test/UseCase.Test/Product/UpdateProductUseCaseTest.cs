using Application.Product.UseCases;
using Core.Entities;
using Core.Repositories;
using Core.Shared.Error;
using Moq;
using TestUtilities.Builder;
using Xunit;

namespace UseCase.Test.Product;

public class UpdateProductUseCaseTest
{
    [Fact]
    public async Task ExecuteAsync_ShouldReturnSuccess_WhenRequestIsValid()
    {
        // Arrange
        var request = UpdateProductRequestBuilder.Build();
        var product = ProductBuilder.Build();
        var category = CategoryBuilder.Build();

        // Match IDs
        var prodIdProp = product.GetType().GetProperty("Id");
        prodIdProp?.SetValue(product, request.Id);

        var catIdProp = category.GetType().GetProperty("Id");
        catIdProp?.SetValue(category, request.CategoryId);

        var productRepositoryMock = new Mock<IProductRepository>();
        var categoryRepositoryMock = new Mock<ICategoryRepository>();
        var unitOfWorkMock = new Mock<IUnitOfWork>();

        productRepositoryMock.Setup(r => r.GetByIdAsync(request.Id))
            .ReturnsAsync(product);

        categoryRepositoryMock.Setup(r => r.GetCategoryByIdAsync(request.CategoryId))
            .ReturnsAsync(category);

        var useCase = new UpdateProductUseCase(productRepositoryMock.Object, categoryRepositoryMock.Object, unitOfWorkMock.Object);

        // Act
        var result = await useCase.ExecuteAsync(request);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(request.Name, result.Data!.Name);
        Assert.Equal(request.Description, result.Data.Description);

        productRepositoryMock.Verify(r => r.UpdateAsync(It.IsAny<Core.Entities.Product>()), Times.Once);
        unitOfWorkMock.Verify(u => u.CommitAsync(), Times.Once);
    }

    [Fact]
    public async Task ExecuteAsync_ShouldReturnFailure_WhenProductNotFound()
    {
        // Arrange
        var request = UpdateProductRequestBuilder.Build();

        var productRepositoryMock = new Mock<IProductRepository>();
        var categoryRepositoryMock = new Mock<ICategoryRepository>();
        var unitOfWorkMock = new Mock<IUnitOfWork>();

        productRepositoryMock.Setup(r => r.GetByIdAsync(request.Id))
            .ReturnsAsync((Core.Entities.Product?)null);

        var useCase = new UpdateProductUseCase(productRepositoryMock.Object, categoryRepositoryMock.Object, unitOfWorkMock.Object);

        // Act
        var result = await useCase.ExecuteAsync(request);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(ErrorMessages.PRODUCT_NOT_FOUND, result.Error!.Errors.First());

        productRepositoryMock.Verify(r => r.UpdateAsync(It.IsAny<Core.Entities.Product>()), Times.Never);
        unitOfWorkMock.Verify(u => u.CommitAsync(), Times.Never);
    }

    [Fact]
    public async Task ExecuteAsync_ShouldReturnFailure_WhenCategoryNotFound()
    {
        // Arrange
        var request = UpdateProductRequestBuilder.Build();
        var product = ProductBuilder.Build();
        var prodIdProp = product.GetType().GetProperty("Id");
        prodIdProp?.SetValue(product, request.Id);

        var productRepositoryMock = new Mock<IProductRepository>();
        var categoryRepositoryMock = new Mock<ICategoryRepository>();
        var unitOfWorkMock = new Mock<IUnitOfWork>();

        productRepositoryMock.Setup(r => r.GetByIdAsync(request.Id))
            .ReturnsAsync(product);

        categoryRepositoryMock.Setup(r => r.GetCategoryByIdAsync(request.CategoryId))
            .ReturnsAsync((Core.Entities.Category?)null);

        var useCase = new UpdateProductUseCase(productRepositoryMock.Object, categoryRepositoryMock.Object, unitOfWorkMock.Object);

        // Act
        var result = await useCase.ExecuteAsync(request);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal("Categoria não encontrada", result.Error!.Errors.First());

        productRepositoryMock.Verify(r => r.UpdateAsync(It.IsAny<Core.Entities.Product>()), Times.Never);
        unitOfWorkMock.Verify(u => u.CommitAsync(), Times.Never);
    }
}
