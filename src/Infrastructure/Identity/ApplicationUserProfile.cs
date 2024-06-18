using AssetManagement.Application.Users.Queries.GetUser;
using AssetManagement.Domain.Enums;
using AutoMapper;

namespace AssetManagement.Infrastructure.Identity;

public class ApplicationUserProfile : Profile
{
    public ApplicationUserProfile()
    {
        CreateMap<ApplicationUser, UserDto>().ReverseMap(); ;
    }
}