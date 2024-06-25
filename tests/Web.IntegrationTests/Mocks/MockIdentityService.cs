using AssetManagement.Application.Auth.Queries.GetCurrentUserInfo;
using AssetManagement.Application.Common.Interfaces;
using AssetManagement.Application.Common.Models;
using AssetManagement.Application.Users.Commands.Create;
using AssetManagement.Application.Users.Queries.GetUser;
using AssetManagement.Application.Users.Queries.GetUsers;
using AssetManagement.Application.Users.Queries.GetUsersBySearch;
using AssetManagement.Application.Users.Queries.GetUsersByType;

namespace Web.IntegrationTests.Mocks;

public class MockIdentityService : IIdentityService
{
    public Task<bool> AuthorizeAsync(string userId, string policyName)
    {
        throw new NotImplementedException();
    }

    public Task<Result> ChangePasswordAsync(string currentPassword, string newPassword)
    {
        throw new NotImplementedException();
    }

    public Task<Result> ChangePasswordFirstTimeAsync(string newPassword)
    {
        throw new NotImplementedException();
    }

    public Task<bool> CheckCurrentPassword(string currentPassword)
    {
        throw new NotImplementedException();
    }

    public Task<(Result Result, string Id)> CreateUserAsync(CreateUserDto createUser)
    {
        throw new NotImplementedException();
    }

    public Task<Result> DeleteUserAsync(string userId)
    {
        throw new NotImplementedException();
    }

    public Task<UserInfoDto> GetCurrentUserInfo(string userId)
    {
        throw new NotImplementedException();
    }

    public Task<PaginatedList<UserBriefDto>> GetUserBriefsAsync(GetUsersQuery query)
    {
        throw new NotImplementedException();
    }

    public Task<PaginatedList<UserBriefDto>> GetUserBriefsBySearchAsync(GetUsersBySearchQuery query)
    {
        throw new NotImplementedException();
    }

    public Task<string?> GetUserNameAsync(string userId)
    {
        throw new NotImplementedException();
    }

    public Task<PaginatedList<UserBriefDto>> GetUsersByTypesAsync(GetUsersByTypeQuery query)
    {
        throw new NotImplementedException();
    }

    public Task<List<string?>> GetUserTypes()
    {
        throw new NotImplementedException();
    }

    public Task<UserDto> GetUserWithRoleAsync(string userId)
    {
        throw new NotImplementedException();
    }

    public Task<bool> IsInRoleAsync(string userId, string role)
    {
        throw new NotImplementedException();
    }

    public Task<bool> IsSameOldPassword(string newPassword)
    {
        throw new NotImplementedException();
    }

    public Task<bool> IsUserDisabledAsync(string email)
    {
        throw new NotImplementedException();
    }

    public Task Logout()
    {
        throw new NotImplementedException();
    }

    public Task<Result> UpdateUserAsync(UserDto userDto)
    {
        throw new NotImplementedException();
    }

    public Task<Result> UpdateUserToRoleAsync(string userId, string currentRole, string newRole)
    {
        throw new NotImplementedException();
    }
}