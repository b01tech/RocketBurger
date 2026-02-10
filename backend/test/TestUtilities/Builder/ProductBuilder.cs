using Bogus;
using Core.Entities;
using Core.Shared.ValueObjects;

namespace TestUtilities.Builder;

public class ProductBuilder
{
    public static Product Build(long? categoryId = null)
    {
        var faker = new Faker();
        var name = faker.Commerce.ProductName();
        var description = faker.Commerce.ProductDescription();
        var imageUrl = faker.Internet.Avatar();
        var price = Price.Create(decimal.Parse(faker.Commerce.Price(1, 100))).Data!;
        var catId = categoryId ?? faker.Random.Long(1, 100);

        var product = Product.Create(name, description, imageUrl, price, catId).Data!;

        return product;
    }
}
