namespace Application.Product.Dtos;

public record CreateProductRequest(string Name, string? Description, decimal Price, string? ImageUrl, long CategoryId, int StockQuantity);
