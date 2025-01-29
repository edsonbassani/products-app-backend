using FluentValidation;

namespace DeveloperEvaluation.WebApi.Features.Products.ListProduct;

/// <summary>
/// Validator for ListProductRequest
/// </summary>
public class ListProductRequestValidator : AbstractValidator<ListProductRequest>
{
    /// <summary>
    /// Initializes validation rules for GetProductRequest
    /// </summary>
    public ListProductRequestValidator()
    {
    }
}
