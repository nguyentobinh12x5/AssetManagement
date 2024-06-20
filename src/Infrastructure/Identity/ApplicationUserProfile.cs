using AssetManagement.Application.Auth.Queries.GetCurrentUserInfo;
using AssetManagement.Application.Users.Queries.GetUsers;
using AssetManagement.Infrastructure.Identity;

using AutoMapper;

public class ApplicationUserProfile : Profile
{
    public ApplicationUserProfile()
    {
        CreateMap<ApplicationUser, UserDto>().ReverseMap(); ;
        CreateMap<ApplicationUser, UserBriefDto>()
            .ForMember(
                dest => dest.FullName,
                opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}")
            );
        CreateMap<ApplicationUser, UserBriefDto>();
        CreateMap<UserInfoDto, ApplicationUser>();
    }
}