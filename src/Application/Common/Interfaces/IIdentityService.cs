using AssetManagement.Application.Common.Models;

namespace AssetManagement.Application.Common.Interfaces;

public interface IIdentityService
{
    Task Logout();

    Task<string?> GetUserNameAsync(string userId);

    Task<bool> IsInRoleAsync(string userId, string role);

    Task<bool> AuthorizeAsync(string userId, string policyName);

    Task<(Result Result, string UserId)> CreateUserAsync(string userName, string password);

    Task<Result> DeleteUserAsync(string userId);
    Task<bool> IsUserDisabledAsync(string email);
}
