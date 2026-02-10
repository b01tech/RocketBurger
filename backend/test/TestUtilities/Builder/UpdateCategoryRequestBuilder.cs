using Application.Category.Dtos;
using Bogus;

namespace TestUtilities.Builder;

public class UpdateCategoryRequestBuilder
{
    public static UpdateCategoryRequest Build(string? inputDescription = null)
    {
        var faker = new Faker();
        var description = inputDescription ?? faker.Commerce.ProductAdjective();
        var categoryRequest = new UpdateCategoryRequest(description);
        return categoryRequest;
    }
}
