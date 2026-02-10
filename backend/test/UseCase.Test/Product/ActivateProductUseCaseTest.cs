using Application.Product.UseCases;
using Core.Repositories;
using Moq;
using Xunit;

namespace UseCase.Test.Product;

public class ActivateProductUseCaseTest
{
    [Fact]
    public async Task ExecuteAsync_ShouldReturnSuccess_WhenProductExists()
    {
        // Arrange
        long productId = 1;
        var productRepositoryMock = new Mock<IProductRepository>();
        var unitOfWorkMock = new Mock<IUnitOfWork>();

        productRepositoryMock.Setup(r => r.ActivateAsync(productId))
            .ReturnsAsync(true);

        var useCase = new ActivateProductUseCase(productRepositoryMock.Object, unitOfWorkMock.Object);

        // Act
        var result = await useCase.ExecuteAsync(productId);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.True(result.Data);
        
        productRepositoryMock.Verify(r => r.ActivateAsync(productId), Times.Once);
        unitOfWorkMock.Verify(u => u.CommitAsync(), Times.Once);
    }

    [Fact]
    public async Task ExecuteAsync_ShouldReturnFailure_WhenProductNotFound()
    {
        // Arrange
        long productId = 1;
        var productRepositoryMock = new Mock<IProductRepository>();
        var unitOfWorkMock = new Mock<IUnitOfWork>();

        productRepositoryMock.Setup(r => r.ActivateAsync(productId))
            .ReturnsAsync(false);

        var useCase = new ActivateProductUseCase(productRepositoryMock.Object, unitOfWorkMock.Object);

        // Act
        var result = await useCase.ExecuteAsync(productId);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal("Produto não encontrado", result.Error!.Errors.First());
        
        unitOfWorkMock.Verify(u => u.CommitAsync(), Times.Never);
    }
}
