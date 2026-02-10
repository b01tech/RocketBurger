namespace Application.Product.Dtos;

public record ProductResponse(long Id, string Name, string Description, decimal Price, string ImageUrl, long CategoryId, string CategoryName, bool IsActive, int StockQuantity);
