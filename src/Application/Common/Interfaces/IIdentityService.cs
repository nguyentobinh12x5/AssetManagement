using AssetManagement.Application.Common.Models;
using AssetManagement.Application.Users.Queries.GetUsers;

namespace AssetManagement.Application.Common.Interfaces;

public interface IIdentityService
{
    Task Logout();

    Task<string?> GetUserNameAsync(string userId);

    Task<bool> IsInRoleAsync(string userId, string role);

    Task<bool> AuthorizeAsync(string userId, string policyName);

    Task<(Result Result, string UserId)> CreateUserAsync(string userName, string password);

    Task<Result> DeleteUserAsync(string userId);
    
    Task<PaginatedList<UserBriefDto>> GetUserBriefsAsync (GetUsersQuery query);

    Task<bool> IsUserDisabledAsync(string email);
}
