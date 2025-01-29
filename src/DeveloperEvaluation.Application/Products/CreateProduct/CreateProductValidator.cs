using Developerevaluation.Domain.Entities;
using Developerevaluation.Domain.Enums;
using Developerevaluation.Domain.Repositories;
using Developerevaluation.Domain.Validation;
using FluentValidation;

namespace Developerevaluation.Application.Products.CreateProduct;

/// <summary>
/// Validator for CreateProductCommand that defines validation rules for product creation command.
/// </summary>
public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    private readonly IProductRepository _productRepository;

    /// <summary>
    /// Initializes a new instance of the CreateProductCommandValidator with defined validation rules.
    /// </summary>
    /// <remarks>
    /// Validation rules include:
    /// - Name: Required, must be between 5 and 50 characters
    /// - Phone: Must be greater than 0 (zero)
    /// - Status: Cannot be set to Unknown
    /// </remarks>
    public CreateProductCommandValidator(IProductRepository productRepository)
    {
        _productRepository = productRepository;

        RuleFor(request => new Product
        {
            Name = request.Name,
            Price = request.Price,
            Active = request.Active
        }).SetValidator(new ProductValidator());

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Product name is required.")
            .MustAsync(BeUniqueName).WithMessage("The product already exists.");
    }

    private async Task<bool> BeUniqueName(string productName, CancellationToken cancellationToken)
    {
        return !await _productRepository.ExistsAsync(productName, cancellationToken);
    }
}