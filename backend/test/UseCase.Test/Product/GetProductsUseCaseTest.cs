using Application.Product.UseCases;
using Core.Repositories;
using Moq;
using TestUtilities.Builder;
using Xunit;

namespace UseCase.Test.Product;

public class GetProductsUseCaseTest
{
    [Fact]
    public async Task ExecuteAsync_ShouldReturnPagedProducts_WhenProductsExist()
    {
        // Arrange
        int page = 1;
        int pageSize = 10;
        var products = new List<Core.Entities.Product>
        {
            ProductBuilder.Build(),
            ProductBuilder.Build(),
            ProductBuilder.Build()
        };
        int totalItems = 3;

        var productRepositoryMock = new Mock<IProductRepository>();

        productRepositoryMock.Setup(r => r.GetAllActiveAsync(page, pageSize))
            .ReturnsAsync(products);
            
        productRepositoryMock.Setup(r => r.CountActiveAsync())
            .ReturnsAsync(totalItems);

        var useCase = new GetProductsUseCase(productRepositoryMock.Object);

        // Act
        var result = await useCase.ExecuteAsync(page, pageSize);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Data);
        Assert.Equal(products.Count, result.Data.Items.Count());
        Assert.Equal(page, result.Data.Pagination.Page);
        Assert.Equal(pageSize, result.Data.Pagination.PageSize);
        Assert.Equal(totalItems, result.Data.Pagination.TotalItems);
        Assert.Equal(1, result.Data.Pagination.TotalPages); // 3 items, page size 10 => 1 page
    }

    [Fact]
    public async Task ExecuteAsync_ShouldReturnEmptyList_WhenNoProductsExist()
    {
        // Arrange
        int page = 1;
        int pageSize = 10;
        var products = new List<Core.Entities.Product>();
        int totalItems = 0;

        var productRepositoryMock = new Mock<IProductRepository>();

        productRepositoryMock.Setup(r => r.GetAllActiveAsync(page, pageSize))
            .ReturnsAsync(products);
            
        productRepositoryMock.Setup(r => r.CountActiveAsync())
            .ReturnsAsync(totalItems);

        var useCase = new GetProductsUseCase(productRepositoryMock.Object);

        // Act
        var result = await useCase.ExecuteAsync(page, pageSize);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Data);
        Assert.Empty(result.Data.Items);
        Assert.Equal(0, result.Data.Pagination.TotalItems);
    }
}
