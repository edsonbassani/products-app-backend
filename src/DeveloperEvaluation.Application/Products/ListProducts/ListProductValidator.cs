using FluentValidation;

namespace DeveloperEvaluation.Application.Products.ListProduct;

/// <summary>
/// Validator for ListProductCommand
/// </summary>
public class ListProductValidator : AbstractValidator<ListProductCommand>
{
    /// <summary>
    /// Initializes validation rules for GetProductCommand
    /// </summary>
    public ListProductValidator()
    {
    }
}
