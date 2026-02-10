using Application.Product.Dtos;
using Bogus;

namespace TestUtilities.Builder;

public class CreateProductRequestBuilder
{
    public static CreateProductRequest Build(long? categoryId = null, int? stockQuantity = null)
    {
        var faker = new Faker();
        var name = faker.Commerce.ProductName();
        var description = faker.Commerce.ProductDescription();
        var price = decimal.Parse(faker.Commerce.Price(1, 100));
        var imageUrl = faker.Internet.Avatar();
        var catId = categoryId ?? faker.Random.Long(1, 100);
        var stock = stockQuantity ?? faker.Random.Int(0, 100);

        return new CreateProductRequest(name, description, price, imageUrl, catId, stock);
    }
}
