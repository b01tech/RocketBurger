using Application.Product.Dtos;
using Bogus;

namespace TestUtilities.Builder;

public class UpdateProductRequestBuilder
{
    public static UpdateProductRequest Build(long? id = null, long? categoryId = null)
    {
        var faker = new Faker();
        var productId = id ?? faker.Random.Long(1, 1000);
        var name = faker.Commerce.ProductName();
        var description = faker.Commerce.ProductDescription();
        var price = decimal.Parse(faker.Commerce.Price(1, 100));
        var imageUrl = faker.Internet.Avatar();
        var catId = categoryId ?? faker.Random.Long(1, 100);

        return new UpdateProductRequest(productId, name, description, price, imageUrl, catId);
    }
}
