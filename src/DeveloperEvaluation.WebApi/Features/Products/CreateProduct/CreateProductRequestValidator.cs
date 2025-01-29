using Developerevaluation.Domain.Entities;
using Developerevaluation.Domain.Enums;
using Developerevaluation.Domain.Validation;
using FluentValidation;

namespace Developerevaluation.WebApi.Features.Products.CreateProduct;

/// <summary>
/// Validator for CreateProductRequest that defines validation rules for product creation.
/// </summary>
public class CreateProductRequestValidator : AbstractValidator<CreateProductRequest>
{
    /// <summary>
    /// Initializes a new instance of the CreateProductRequestValidator with defined validation rules.
    /// </summary>
    /// <remarks>
    /// Validation rules include:
    /// - Name: Must be valid format (using EmailValidator)
    /// - Price: Greater than 0 (zero)
    /// </remarks>
    public CreateProductRequestValidator()
    {
        RuleFor(request => new Product
        {
            Name = request.Name,
            Price = request.Price,
            Active = request.Active
        }).SetValidator(new ProductValidator());
    }
}