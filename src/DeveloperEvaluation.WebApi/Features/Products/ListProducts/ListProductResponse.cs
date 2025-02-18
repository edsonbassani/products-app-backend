using DeveloperEvaluation.Domain.Enums;

namespace DeveloperEvaluation.WebApi.Features.Products.ListProduct;

/// <summary>
/// API response model for ListProduct operation
/// </summary>
public class ListProductResponse
{
    /// <summary>
    /// The unique identifier of the product
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// The product's name
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// The product's price
    /// </summary>
    public double Price { get; set; } = 0;

    /// <summary>
    /// The product's status
    /// </summary>
    public bool Active { get; set; }
}
