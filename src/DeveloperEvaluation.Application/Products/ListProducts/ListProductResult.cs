using DeveloperEvaluation.Domain.Entities;
using DeveloperEvaluation.Domain.Enums;

namespace DeveloperEvaluation.Application.Products.ListProduct;

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