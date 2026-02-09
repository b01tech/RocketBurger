using Bogus;
using Core.Entities;

namespace TestUtilities.Builder;

public class CategoryBuilder
{
    public static Category Build(string? inputName = null, string? inputDescription = null)
    {
        var faker = new Faker();
        var name = inputName ?? faker.Commerce.Department();
        var description = inputDescription ?? faker.Commerce.ProductAdjective();
        var category = Category.Create(name, description);
        return category.Data!;
    }
}
