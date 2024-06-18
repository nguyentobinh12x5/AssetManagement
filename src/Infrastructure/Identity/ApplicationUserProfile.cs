using AssetManagement.Infrastructure.Identity;
using AssetManagement.Application.Auth.Queries.GetCurrentUserInfo;
using AutoMapper;

public class ApplicationUserProfile : Profile
{
    public ApplicationUserProfile()
    {
        CreateMap<UserInfoDto, ApplicationUser>();
    }
}