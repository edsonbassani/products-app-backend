using DeveloperEvaluation.Domain.Entities;
using DeveloperEvaluation.Domain.Enums;
using FluentValidation;

namespace DeveloperEvaluation.Domain.Validation;

public class ProductValidator : AbstractValidator<Product>
{
    public ProductValidator()
    {
        RuleFor(product => product.Name)
            .NotEmpty()
            .MinimumLength(5).WithMessage("Product must be at least 5 characters long")
            .MaximumLength(50).WithMessage("Product cannot be longer than 20 characters.");

        RuleFor(product => product.Price)
           .GreaterThan(0).WithMessage("Price must be greater than zero.")
           .LessThanOrEqualTo(10000).WithMessage("Price must be less than or equal to 10,000.")
           .Must(price => decimal.TryParse(price.ToString(), out _)).WithMessage("Price must be a valid decimal value.");
    }
}
