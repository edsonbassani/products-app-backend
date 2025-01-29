using AutoMapper;
using MediatR;
using FluentValidation;
using DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using DeveloperEvaluation.Domain.Entities;

namespace DeveloperEvaluation.Application.Products.ListProduct;

/// <summary>
/// Handler for processing GetProductCommand requests
/// </summary>
public class ListProductHandler : IRequestHandler<ListProductCommand, ListProductResult>
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of GetProductHandler
    /// </summary>
    /// <param name="productRepository">The product repository</param>
    /// <param name="mapper">The AutoMapper instance</param>
    /// <param name="validator">The validator for GetProductCommand</param>
    public ListProductHandler(
        IProductRepository productRepository,
        IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    /// <summary>
    /// Handles the GetProductCommand request
    /// </summary>
    /// <param name="request">The GetProduct command</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The product details if found</returns>
    public async Task<ListProductResult> Handle(ListProductCommand request, CancellationToken cancellationToken)
    {
        var productsQuery = _productRepository.List();

        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
        {
            productsQuery = productsQuery.Where(p =>
                p.Name.Contains(request.SearchTerm) ||
                p.Name.Contains(request.SearchTerm));
        }

        productsQuery = request.SortBy switch
        {
            "Price" => request.SortDescending
                ? productsQuery.OrderByDescending(p => p.Price)
                : productsQuery.OrderBy(p => p.Price),
            "Name" => request.SortDescending
                ? productsQuery.OrderByDescending(p => p.Name)
                : productsQuery.OrderBy(p => p.Name),
            _ => productsQuery
        };

        var totalItems = await productsQuery.Where(_ => _.Active == true).CountAsync(cancellationToken);
        var products = await productsQuery.Where(_ => _.Active == true)
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);


        var productsDtos = _mapper.Map<List<Product>>(products);

        return new ListProductResult
        {
            Products = productsDtos,
            TotalItems = totalItems,
            Page = request.Page,
            PageSize = request.PageSize
        };

    }
}
