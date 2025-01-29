using AutoMapper;

namespace Developerevaluation.WebApi.Features.Products.ListProduct;

/// <summary>
/// Profile for mapping ListProduct feature requests to commands
/// </summary>
public class ListProductProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for GetProduct feature
    /// </summary>
    public ListProductProfile()
    {
        CreateMap<Guid, Application.Products.ListProduct.ListProductCommand>()
            .ConstructUsing(id => new Application.Products.ListProduct.ListProductCommand());
    }
}
