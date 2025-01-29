using Developerevaluation.Application.Users.CreateUser;
using Developerevaluation.WebApi.Features.Users.CreateUser;
using AutoMapper;

namespace Developerevaluation.WebApi.Mappings;

public class CreateUserRequestProfile : Profile
{
    public CreateUserRequestProfile()
    {
        CreateMap<CreateUserRequest, CreateUserCommand>();
    }
}