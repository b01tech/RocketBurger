using Application.Product.UseCases;
using Core.Repositories;
using Moq;
using TestUtilities.Builder;
using Xunit;

namespace UseCase.Test.Product;

public class GetProductByIdUseCaseTest
{
    [Fact]
    public async Task ExecuteAsync_ShouldReturnSuccess_WhenProductExists()
    {
        // Arrange
        var product = ProductBuilder.Build();
        var productRepositoryMock = new Mock<IProductRepository>();

        productRepositoryMock.Setup(r => r.GetByIdAsync(product.Id))
            .ReturnsAsync(product);

        var useCase = new GetProductByIdUseCase(productRepositoryMock.Object);

        // Act
        var result = await useCase.ExecuteAsync(product.Id);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Data);
        Assert.Equal(product.Id, result.Data.Id);
        Assert.Equal(product.Name, result.Data.Name);
    }

    [Fact]
    public async Task ExecuteAsync_ShouldReturnFailure_WhenProductNotFound()
    {
        // Arrange
        long productId = 999;
        var productRepositoryMock = new Mock<IProductRepository>();

        productRepositoryMock.Setup(r => r.GetByIdAsync(productId))
            .ReturnsAsync((Core.Entities.Product?)null);

        var useCase = new GetProductByIdUseCase(productRepositoryMock.Object);

        // Act
        var result = await useCase.ExecuteAsync(productId);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal("Produto não encontrado", result.Error!.Errors.First());
    }
}
