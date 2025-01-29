using Developerevaluation.Domain.Entities;
using Developerevaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Developerevaluation.ORM.Repositories;

/// <summary>
/// Implementation of IProductRepository using Entity Framework Core
/// </summary>
public class ProductRepository : IProductRepository
{
    private readonly DefaultContext _context;

    /// <summary>
    /// Initializes a new instance of ProductRepository
    /// </summary>
    /// <param name="context">The database context</param>
    public ProductRepository(DefaultContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Creates a new product in the database
    /// </summary>
    /// <param name="product">The product to create</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created product</returns>
    public async Task<Product> CreateAsync(Product product, CancellationToken cancellationToken = default)
    {
        await _context.Products.AddAsync(product, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return product;
    }

    /// <summary>
    /// Retrieves a product by their unique identifier
    /// </summary>
    /// <param name="id">The unique identifier of the product</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The product if found, null otherwise</returns>
    public async Task<Product?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Products.FirstOrDefaultAsync(o=> o.Id == id, cancellationToken);
    }

    /// <summary>
    /// Retrieves a product by their name
    /// </summary>
    /// <param name="name">The name of the product</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The product if found, null otherwise</returns>
    public async Task<Product?> GetByNameAsync(string name, CancellationToken cancellationToken = default)
    {
        return await _context.Products.FirstOrDefaultAsync(o=> o.Name == name, cancellationToken);
    }

    /// <summary>
    /// Retrieves the product list
    /// </summary>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The products list</returns>
    public IQueryable<Product> List(CancellationToken cancellationToken = default)
    {
        return _context.Products.AsQueryable();
    }

    /// <summary>
    /// Deletes a product from the database
    /// </summary>
    /// <param name="id">The unique identifier of the product to delete</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>True if the product was deleted, false if not found</returns>
    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        // Retrieve the product by ID
        var product = await GetByIdAsync(id, cancellationToken);
        if (product == null)
            return false;

        // Update the Active flag to false
        product.Active = false;

        // Mark the product as modified
        _context.Products.Update(product);

        // Save changes to the database
        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }

    /// <summary>
    /// Checks for an existing product
    /// </summary>
    /// <param name="name">The product name</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>True if the product is found, false if not found</returns>
    public async Task<bool> ExistsAsync(string name, CancellationToken cancellationToken)
    {
        return await _context.Products.AnyAsync(p => p.Name == name, cancellationToken);
    }
}
