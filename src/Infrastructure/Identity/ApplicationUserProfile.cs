using AssetManagement.Application.Auth.Queries.GetCurrentUserInfo;
using AssetManagement.Infrastructure.Identity;

using AutoMapper;

public class ApplicationUserProfile : Profile
{
    public ApplicationUserProfile()
    {
        CreateMap<UserInfoDto, ApplicationUser>();
    }
}