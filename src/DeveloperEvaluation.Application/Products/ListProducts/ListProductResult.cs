using Developerevaluation.Domain.Entities;
using Developerevaluation.Domain.Enums;

namespace Developerevaluation.Application.Products.ListProduct;

/// <summary>
/// Response model for GetProduct operation
/// </summary>
public class ListProductResult
{
    public List<Product> Products { get; init; } = new();
    public int TotalItems { get; init; }
    public int Page { get; init; }
    public int PageSize { get; init; }
}