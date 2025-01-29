using Developerevaluation.Domain.Enums;

namespace Developerevaluation.Application.Products.GetProduct;

/// <summary>
/// Response model for GetProduct operation
/// </summary>
public class GetProductResult
{
    /// <summary>
    /// The unique identifier of the product
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// The product's full name
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// The product's email address
    /// </summary>
    public double Price { get; set; } = 0;

    /// <summary>
    /// The current status of the product
    /// </summary>
    public bool Active { get; set; }
}
