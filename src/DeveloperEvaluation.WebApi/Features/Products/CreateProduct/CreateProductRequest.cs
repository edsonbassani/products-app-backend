using Developerevaluation.Domain.Enums;

namespace Developerevaluation.WebApi.Features.Products.CreateProduct;

/// <summary>
/// Represents a request to create a new product in the system.
/// </summary>
public class CreateProductRequest
{
    /// <summary>
    /// Gets or sets the product name.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the product's price.
    /// </summary>
    public double Price { get; set; } = 0;

    /// <summary>
    /// Gets or sets the product's status
    /// </summary>
    public bool Active { get; set; }
}