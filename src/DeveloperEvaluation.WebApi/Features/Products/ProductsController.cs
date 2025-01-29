using MediatR;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using DeveloperEvaluation.WebApi.Common;
using DeveloperEvaluation.WebApi.Features.Products.CreateProduct;
using DeveloperEvaluation.WebApi.Features.Products.GetProduct;
using DeveloperEvaluation.WebApi.Features.Products.DeleteProduct;
using DeveloperEvaluation.Application.Products.CreateProduct;
using DeveloperEvaluation.Application.Products.GetProduct;
using DeveloperEvaluation.Application.Products.DeleteProduct;
using Rebus.Bus;
using DeveloperEvaluation.WebApi.Features.Products.ListProduct;
using DeveloperEvaluation.Application.Products.ListProduct;
using System.ComponentModel.DataAnnotations;

namespace DeveloperEvaluation.WebApi.Features.Products;

/// <summary>
/// Controller for managing product operations
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class ProductsController : BaseController
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly IBus _bus;
    private readonly ILogger<ProductsController> _logger;

    /// <summary>
    /// Initializes a new instance of ProductsController
    /// </summary>
    /// <param name="mediator">The mediator instance</param>
    /// <param name="mapper">The AutoMapper instance</param>
    public ProductsController(IMediator mediator, IMapper mapper, IBus bus, ILogger<ProductsController> logger)
    {
        _mediator = mediator;
        _mapper = mapper;
        _bus = bus;
        _logger = logger;
    }

    /// <summary>
    /// Creates a new product
    /// </summary>
    /// <param name="request">The product creation request</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created product details</returns>
    [HttpPost]
    [ProducesResponseType(typeof(ApiResponseWithData<CreateProductResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateProduct([FromBody] CreateProductRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var validator = new CreateProductRequestValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var command = _mapper.Map<CreateProductCommand>(request);
            var response = await _mediator.Send(command, cancellationToken);
            await _bus.Publish(response);

            _logger.LogInformation("Product created with ID {ProductId}", response.Id);
            return Created(string.Empty, new ApiResponseWithData<CreateProductResponse>
            {
                Success = true,
                Message = "Product created successfully",
                Data = _mapper.Map<CreateProductResponse>(response)
            });
        }
        catch (ValidationException ex)
        {
            return BadRequest(new ApiResponse
            {
                Success = false,
                Message = ex.Message
            });
        }

    }

    /// <summary>
    /// Retrieves a product by their ID
    /// </summary>
    /// <param name="id">The unique identifier of the product</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The product details if found</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ApiResponseWithData<GetProductResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetProduct([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var request = new GetProductRequest { Id = id };
        var validator = new GetProductRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var command = _mapper.Map<GetProductCommand>(request.Id);
        var response = await _mediator.Send(command, cancellationToken);

        return Ok(new ApiResponseWithData<GetProductResponse>
        {
            Success = true,
            Message = "Product retrieved successfully",
            Data = _mapper.Map<GetProductResponse>(response)
        });
    }

    /// <summary>
    /// List products
    /// </summary>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Success response if the product list is found</returns>
    [HttpGet]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ListProductResult), StatusCodes.Status200OK)]
    public async Task<IActionResult> ListProducts(
    CancellationToken cancellationToken,
    [FromQuery] string? searchTerm,
    [FromQuery] string? sortBy,
    [FromQuery] bool sortDescending,
    [FromQuery] int page = 1,
    [FromQuery] int pageSize = 10)
    {
        var request = new ListProductCommand
        {
            SearchTerm = searchTerm,
            SortBy = sortBy,
            SortDescending = sortDescending,
            Page = page,
            PageSize = pageSize
        };

        var result = await _mediator.Send(request, cancellationToken);

        return Ok(result);
    }

    /// <summary>
    /// Deactivate a product by their ID
    /// </summary>
    /// <param name="id">The unique identifier of the product to delete</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Success response if the product was deleted</returns>
    [HttpPost("{id}")]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeactivateProduct([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var request = new DeleteProductRequest { Id = id };
        var validator = new DeleteProductRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var command = _mapper.Map<DeleteProductCommand>(request.Id);
        await _mediator.Send(command, cancellationToken);
        _logger.LogInformation("Product deactivated with ID {ProductId}", request.Id);
        return Ok(new ApiResponse
        {
            Success = true,
            Message = "Product successfully deleted"
        });
    }

}
