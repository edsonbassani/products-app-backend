using DeveloperEvaluation.Common.Validation;
using DeveloperEvaluation.Domain.Enums;
using DeveloperEvaluation.Domain.Repositories;
using MediatR;

namespace DeveloperEvaluation.Application.Products.CreateProduct;

/// <summary>
/// Command for creating a new product.
/// </summary>
/// <remarks>
/// This command is used to capture the required data for creating a product, 
/// including name, value and status
/// It implements <see cref="IRequest{TResponse}"/> to initiate the request 
/// that returns a <see cref="CreateProductResult"/>.
/// 
/// The data provided in this command is validated using the 
/// <see cref="CreateProductCommandValidator"/> which extends 
/// <see cref="AbstractValidator{T}"/> to ensure that the fields are correctly 
/// populated and follow the required rules.
/// </remarks>
public class CreateProductCommand : IRequest<CreateProductResult>
{
    /// <summary>
    /// Gets or sets the name of the product to be created.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the price of the product.
    /// </summary>
    public double Price { get; set; } = 0;

    /// <summary>
    /// Gets or sets the status of the product.
    /// </summary>
    public bool Active { get; set; }

    public ValidationResultDetail Validate(IProductRepository productRepository)
    {
        var validator = new CreateProductCommandValidator(productRepository);
        var result = validator.Validate(this);
        return new ValidationResultDetail
        {
            IsValid = result.IsValid,
            Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
        };
    }
}