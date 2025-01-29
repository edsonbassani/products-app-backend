using MediatR;
using FluentValidation;
using Developerevaluation.Domain.Repositories;
using Developerevaluation.Application.Products.DeleteProduct;
using Microsoft.Extensions.Logging;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Developerevaluation.Application.Users.DeleteUser;

/// <summary>
/// Handler for processing DeleteUserCommand requests
/// </summary>
public class DeleteUserHandler : IRequestHandler<DeleteUserCommand, DeleteUserResponse>
{
    private readonly IUserRepository _userRepository;
    private readonly ILogger<DeleteUserHandler> _logger;

    /// <summary>
    /// Initializes a new instance of DeleteUserHandler
    /// </summary>
    /// <param name="userRepository">The user repository</param>
    /// <param name="validator">The validator for DeleteUserCommand</param>
    public DeleteUserHandler(
        IUserRepository userRepository, ILogger<DeleteUserHandler> logger)
    {
        _userRepository = userRepository;
        _logger = logger;
    }

    /// <summary>
    /// Handles the DeleteUserCommand request
    /// </summary>
    /// <param name="request">The DeleteUser command</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The result of the delete operation</returns>
    public async Task<DeleteUserResponse> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Starting deletion of user with ID {UserId}", request.Id);
            var validator = new DeleteUserValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var success = await _userRepository.DeleteAsync(request.Id, cancellationToken);
            if (!success)
            {
                _logger.LogWarning("User with ID {UserId} not found", request.Id);
                throw new KeyNotFoundException($"User with ID {request.Id} not found");

            }

            _logger.LogInformation("Successfully deleted user with ID {ProductId}", request.Id);
            return new DeleteUserResponse { Success = true };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while deleting user with ID {UserId}", request.Id);
            throw;
        }
       
    }
}
