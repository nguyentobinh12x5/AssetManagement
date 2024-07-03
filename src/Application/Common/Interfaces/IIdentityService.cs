using AssetManagement.Application.Auth.Commands.Login;
using AssetManagement.Application.Auth.Queries.GetCurrentUserInfo;
using AssetManagement.Application.Common.Models;
using AssetManagement.Application.Users.Commands.Create;
using AssetManagement.Application.Users.Queries.GetUser;
using AssetManagement.Application.Users.Queries.GetUsers;

namespace AssetManagement.Application.Common.Interfaces;

public interface IIdentityService
{
    Task Logout();
    Task<Result> Login(LoginCommand request);

    Task<string?> GetUserNameAsync(string userId);

    Task<UserDto> GetUserWithRoleAsync(string userId);

    Task<Result> UpdateUserAsync(UserDto userDto);

    Task<Result> UpdateUserToRoleAsync(string userId, string currentRole, string newRole);

    Task<bool> IsInRoleAsync(string userId, string role);

    Task<bool> AuthorizeAsync(string userId, string policyName);

    Task<(Result Result, string Id)> CreateUserAsync(CreateUserDto createUser);

    Task<Result> DeleteUserAsync(string userId);

    Task<PaginatedList<UserBriefDto>> GetUserBriefsAsync(GetUsersQuery query);

    Task<bool> CheckCurrentPassword(string currentPassword);
    Task<bool> IsSameOldPassword(string newPassword);
    Task<Result> ChangePasswordFirstTimeAsync(string newPassword);

    Task<Result> ChangePasswordAsync(string currentPassword, string newPassword);

    Task<bool> IsUserDisabledAsync(string email);

    Task<UserInfoDto> GetCurrentUserInfo(string userId);

    Task<List<string?>> GetUserTypes();

}