using AutoMapper;
using DeveloperEvaluation.Domain.Entities;

namespace DeveloperEvaluation.Application.Products.ListProduct;

/// <summary>
/// Profile for mapping between Product entity and ListProductResponse
/// </summary>
public class ListProductProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for GetProduct operation
    /// </summary>
    public ListProductProfile()
    {
        CreateMap<Product, ListProductResult>();
    }
}
