using AutoMapper;
using DeveloperEvaluation.Domain.Entities;
using DeveloperEvaluation.Application.Auth.AuthenticateUser;

namespace DeveloperEvaluation.WebApi.Features.Auth.AuthenticateUserFeature;

/// <summary>
/// AutoMapper profile for authentication-related mappings
/// </summary>
public sealed class AuthenticateUserProfile : Profile
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AuthenticateUserProfile"/> class
    /// </summary>
    public AuthenticateUserProfile()
    {
        CreateMap<AuthenticateUserResult, AuthenticateUserResponse>()
             .ForMember(dest => dest.Token, opt => opt.MapFrom(src => src.Token))
             .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role))
             .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
             .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email));

        CreateMap<AuthenticateUserRequest, AuthenticateUserCommand>();
    }
}
