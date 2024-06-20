using AssetManagement.Application.Auth.Queries.GetCurrentUserInfo;
using AssetManagement.Application.Users.Commands.Create;
using AssetManagement.Application.Users.Commands.UpdateUser;
using AssetManagement.Application.Users.Queries.GetUser;
using AssetManagement.Application.Users.Queries.GetUsers;

using AutoMapper;

namespace AssetManagement.Infrastructure.Identity;

public class ApplicationUserProfile : Profile
{
    public ApplicationUserProfile()
    {
        CreateMap<ApplicationUser, UserDto>().ReverseMap(); ;
        CreateMap<ApplicationUser, UserBriefDto>();
        CreateMap<UserInfoDto, ApplicationUser>();
        CreateMap<ApplicationUser, CreateUserDto>();
    }
}