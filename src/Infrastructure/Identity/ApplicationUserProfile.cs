using AssetManagement.Application.Users.Queries.GetUsers;
using AutoMapper;

namespace AssetManagement.Infrastructure.Identity;

public class ApplicationUserProfile : Profile
{
    public ApplicationUserProfile()
    {
        //Uncomment this later
        //CreateMap<ApplicationUser, UserDto>();
        CreateMap<ApplicationUser, UserBriefDto>();
    }
}