using DeveloperEvaluation.Common.Security;
using DeveloperEvaluation.Common.Validation;
using DeveloperEvaluation.Domain.Common;
using DeveloperEvaluation.Domain.Enums;
using DeveloperEvaluation.Domain.Interfaces;
using DeveloperEvaluation.Domain.Validation;

namespace DeveloperEvaluation.Domain.Entities;


/// <summary>
/// Represents a product in the application with status and valur information.
/// This entity follows domain-driven design principles and includes business rules validation.
/// </summary>
public class Product : BaseEntity, IProduct
{
    /// <summary>
    /// Get the product's name.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Get the product's price.
    /// Must be a valid decimal value
    /// </summary>
    public double Price { get; set; } = 0 ;

    /// <summary>
    /// Set the product active.
    /// Active or Inactive
    /// </summary>
    public bool Active { get; set; } = false;

    /// <summary>
    /// Gets the date and time when the product was created.
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Gets the date and time of the last update to the product's information.
    /// </summary>
    public DateTime? UpdatedAt { get; set; }


    string IProduct.Id => Id.ToString();
    string IProduct.Name => Name;
    double IProduct.Price => Price;
    bool IProduct.Active => Active;

    /// <summary>
    /// Initializes a new instance of the Product class.
    /// </summary>
    public Product()
    {
        CreatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Performs validation of the product entity using the ProductValidator rules.
    /// </summary>
    /// <returns>
    /// A <see cref="ValidationResultDetail"/> containing:
    /// - IsValid: Indicates whether all validation rules passed
    /// - Errors: Collection of validation errors if any rules failed
    /// </returns>
    /// <remarks>
    /// <listheader>The validation includes checking:</listheader>
    /// <list type="bullet">Name format and length</list>
    /// <list type="bullet">Price format</list>
    /// 
    /// </remarks>
    public ValidationResultDetail Validate()
    {
        var validator = new ProductValidator();
        var result = validator.Validate(this);
        return new ValidationResultDetail
        {
            IsValid = result.IsValid,
            Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
        };
    }

    /// <summary>
    /// Activates a product.
    /// Changes the product's status to Active.
    /// </summary>
    public void Activate()
    {
        Active = true;
        UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Deactivates a product.
    /// Changes the product's status to Inactive.
    /// </summary>
    public void Deactivate()
    {
        Active = false;
        UpdatedAt = DateTime.UtcNow;
    }
}