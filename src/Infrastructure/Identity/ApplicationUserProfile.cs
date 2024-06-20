using AssetManagement.Application.Auth.Queries.GetCurrentUserInfo;
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
    }
}