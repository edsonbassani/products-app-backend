using MediatR;
using FluentValidation;
using Developerevaluation.Domain.Repositories;
using Microsoft.Extensions.Logging;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Developerevaluation.Application.Products.DeleteProduct;

/// <summary>
/// Handler for processing DeleteProductCommand requests
/// </summary>
public class DeleteProductHandler : IRequestHandler<DeleteProductCommand, DeleteProductResponse>
{
    private readonly IProductRepository _productRepository;
    private readonly ILogger<DeleteProductHandler> _logger;

    /// <summary>
    /// Initializes a new instance of DeleteProductHandler
    /// </summary>
    /// <param name="productRepository">The product repository</param>
    /// <param name="validator">The validator for DeleteProductCommand</param>
    public DeleteProductHandler(
        IProductRepository productRepository, ILogger<DeleteProductHandler> logger)
    {
        _productRepository = productRepository;
        _logger = logger;
    }

    /// <summary>
    /// Handles the DeleteProductCommand request
    /// </summary>
    /// <param name="request">The DeleteProduct command</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The result of the delete operation</returns>
    public async Task<DeleteProductResponse> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Starting deletion of product with ID {ProductId}", request.Id);

        try
        {
            var validator = new DeleteProductValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var success = await _productRepository.DeleteAsync(request.Id, cancellationToken);

            _logger.LogInformation("Successfully deleted product with ID {ProductId}", request.Id);

            if (!success)
            {
                _logger.LogWarning("Product with ID {ProductId} not found", request.Id);
                throw new KeyNotFoundException($"Product with ID {request.Id} not found");
            }

            return new DeleteProductResponse { Success = true };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while deleting product with ID {ProductId}", request.Id);
            throw;
        }
        
    }
}
