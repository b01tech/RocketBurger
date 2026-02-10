using Application.Category.Dtos;
using Bogus;

namespace TestUtilities.Builder;

public class CreateCategoryRequestBuilder
{
    public static CreateCategoryRequest Build(string? inputName = null, string? inputDescription = null)
    {
        var faker = new Faker();
        var name = inputName ?? faker.Commerce.Department();
        var description = inputDescription ?? faker.Commerce.ProductAdjective();
        var categoryRequest = new CreateCategoryRequest(name, description);
        return categoryRequest;
    }
}
