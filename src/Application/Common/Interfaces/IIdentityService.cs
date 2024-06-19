using AssetManagement.Application.Common.Models;
using AssetManagement.Application.Users.Commands.Create;
using AssetManagement.Application.Users.Commands.UpdateUser;
using AssetManagement.Application.Users.Queries.GetUser;

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

    Task<(Result Result, string StaffCode)> CreateUserAsync(UserDTOs user);

    Task<Result> DeleteUserAsync(string userId);
    Task<bool> IsUserDisabledAsync(string email);
}
