using AssetManagement.Application.Auth.Queries.GetCurrentUserInfo;
using AssetManagement.Application.Common.Models;
using AssetManagement.Application.Users.Queries.GetUser;
using AssetManagement.Application.Users.Queries.GetUsers;
using AssetManagement.Application.Users.Queries.GetUsersByType;

namespace AssetManagement.Application.Common.Interfaces;

public interface IIdentityService
{
    Task Logout();

    Task<string?> GetUserNameAsync(string userId);

    Task<UserDto> GetUserWithRoleAsync(string userId);

    Task<Result> UpdateUserAsync(UserDto userDto);

    Task<Result> UpdateUserToRoleAsync(string userId, string currentRole, string newRole);

    Task<bool> IsInRoleAsync(string userId, string role);

    Task<bool> AuthorizeAsync(string userId, string policyName);

    Task<(Result Result, string UserId)> CreateUserAsync(string userName, string password);

    Task<Result> DeleteUserAsync(string userId);

    Task<PaginatedList<UserBriefDto>> GetUserBriefsAsync(GetUsersQuery query);
    Task<PaginatedList<UserBriefDto>> GetUsersByTypeAsync(GetUsersByTypeQuery query);

    Task<bool> CheckCurrentPassword(string currentPassword);
    Task<bool> IsSameOldPassword(string newPassword);
    Task<Result> ChangePasswordFirstTimeAsync(string newPassword);

    Task<Result> ChangePasswordAsync(string currentPassword, string newPassword);

    Task<bool> IsUserDisabledAsync(string email);

    Task<UserInfoDto> GetCurrentUserInfo(string userId);

}