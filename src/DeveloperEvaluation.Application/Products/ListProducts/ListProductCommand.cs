using MediatR;

namespace DeveloperEvaluation.Application.Products.ListProduct;

/// <summary>
/// Command for list products
/// </summary>
public record ListProductCommand : IRequest<ListProductResult>
{
    public string? SearchTerm { get; init; }
    public string? SortBy { get; init; }
    public bool SortDescending { get; init; }
    public int Page { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}
