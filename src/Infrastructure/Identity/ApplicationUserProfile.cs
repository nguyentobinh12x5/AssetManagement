using AssetManagement.Application.Users.Commands.Create;
using AssetManagement.Application.Users.Queries.GetUser;
using AutoMapper;

namespace AssetManagement.Infrastructure.Identity;

public class ApplicationUserProfile : Profile
{
    public ApplicationUserProfile()
    {
        CreateMap<ApplicationUser, UserDto>().ReverseMap(); ;
        CreateMap<ApplicationUser, UserDTOs>();
    }
}