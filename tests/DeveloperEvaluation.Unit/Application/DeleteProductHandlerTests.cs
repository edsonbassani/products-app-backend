using Developerevaluation.Application.Products.CreateProduct;
using Developerevaluation.Application.Products.DeleteProduct;
using Developerevaluation.Application.Users.DeleteUser;
using Developerevaluation.Common.Security;
using Developerevaluation.Domain.Entities;
using Developerevaluation.Domain.Repositories;
using Developerevaluation.ORM.Repositories;
using Developerevaluation.Unit.Domain;
using AutoMapper;
using Castle.Core.Logging;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Xunit;

namespace Developerevaluation.Unit.Application;

/// <summary>
/// Contains unit tests for the <see cref="CreateProductHandler"/> class.
/// </summary>
public class DeleteProductHandlerTests
{
    private readonly IProductRepository _productRepository;
    private readonly ILogger<DeleteProductHandler> _logger;
    private readonly DeleteProductHandler _handler;
    

    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteProductHandlerTests"/> class.
    /// Sets up the test dependencies and creates fake data generators.
    /// </summary>
    public DeleteProductHandlerTests()
    {
        _productRepository = Substitute.For<IProductRepository>();
        _logger = Substitute.For<ILogger<DeleteProductHandler>>();
        _handler = new DeleteProductHandler(_productRepository, _logger );
    }

    /// <summary>
    /// Tests that a valid user deletion request deletes the user successfully.
    /// </summary>
    //[Fact(DisplayName = "Given valid Product ID When deactivating product Then deactivates product successfully")]
    //public async Task DeleteProduct_ValidRequest_DeletesProductSuccessfully()
    //{
    //    // Given
    //    var guid = Guid.NewGuid();
    //    var command = new DeleteProductCommand(guid);

    //    _productRepository.DeleteAsync(command.Id, Arg.Any<CancellationToken>())
    //        .Returns(Task.CompletedTask);

    //    // When
    //    await _handler.Handle(command, CancellationToken.None);

    //    // Then
    //    await _productRepository.Received(1).DeleteAsync(command.Id, Arg.Any<CancellationToken>());
    //    _logger.Received(1).LogInformation("Successfully deleted product with ID {ProductId}", command.Id);
    //}
}
