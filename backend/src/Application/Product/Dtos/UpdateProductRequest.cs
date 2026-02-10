namespace Application.Product.Dtos;

public record UpdateProductRequest(long Id, string Name, string? Description, decimal Price, string? ImageUrl, long CategoryId);
