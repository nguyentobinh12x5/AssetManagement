using AssetManagement.Application.Auth.Queries.GetCurrentUserInfo;
using AssetManagement.Application.Common.Interfaces;
using AssetManagement.Application.Common.Models;

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

    public Task<(Result Result, string UserId)> CreateUserAsync(string userName, string password)
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

    public Task<string?> GetUserNameAsync(string userId)
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
        return Task.FromResult(false);
    }

    public Task Logout()
    {
        throw new NotImplementedException();
    }
}