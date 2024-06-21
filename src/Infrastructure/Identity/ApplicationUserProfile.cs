using AssetManagement.Application.Auth.Queries.GetCurrentUserInfo;
using AssetManagement.Application.Users.Queries.GetUsers;
using AssetManagement.Infrastructure.Identity;

using AutoMapper;

namespace AssetManagement.Infrastructure.Identity;

public class ApplicationUserProfile : Profile
{
    public ApplicationUserProfile()
    {
        //Uncomment this later
        //CreateMap<ApplicationUser, UserDto>();
        CreateMap<ApplicationUser, UserBriefDto>();
        CreateMap<UserInfoDto, ApplicationUser>();
    }
}