using Developerevaluation.Application.Products.CreateProduct;
using Developerevaluation.Domain.Enums;
using Bogus;

namespace Developerevaluation.Unit.Domain;

/// <summary>
/// Provides methods for generating test data using the Bogus library.
/// This class centralizes all test data generation to ensure consistency
/// across test cases and provide both valid and invalid data scenarios.
/// </summary>
public static class CreateProductHandlerTestData
{
    /// <summary>
    /// Configures the Faker to generate valid Product entities.
    /// The generated products will have valid:
    /// - Name
    /// - Price
    /// - Status (Active or Inactive)
    /// </summary>
    private static readonly Faker<CreateProductCommand> CreateProductHandlerFaker = new Faker<CreateProductCommand>()
     .RuleFor(p => p.Name, f => f.Commerce.ProductName())
     .RuleFor(p => p.Active, f => f.Random.Bool());

    /// <summary>
    /// Generates a valid Product entity with randomized data.
    /// The generated product will have all properties populated with valid values
    /// that meet the system's validation requirements.
    /// </summary>
    /// <returns>A valid Product entity with randomly generated data.</returns>
    public static CreateProductCommand GenerateValidCommand()
    {
        return CreateProductHandlerFaker.Generate();
    }
}
