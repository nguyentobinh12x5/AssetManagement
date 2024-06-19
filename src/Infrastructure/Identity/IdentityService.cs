using System.Security.Claims;
using AssetManagement.Application.Common.Extensions;
using AssetManagement.Application.Common.Interfaces;
using AssetManagement.Application.Common.Models;
using AssetManagement.Application.Users.Commands.Create;
using AssetManagement.Application.Users.Commands.UpdateUser;
using AssetManagement.Application.Users.Queries.GetUser;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AssetManagement.Infrastructure.Identity;

public class IdentityService : IIdentityService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly IUserClaimsPrincipalFactory<ApplicationUser> _userClaimsPrincipalFactory;
    private readonly IAuthorizationService _authorizationService;
    private readonly IMapper _mapper;

    public IdentityService(
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        IUserClaimsPrincipalFactory<ApplicationUser> userClaimsPrincipalFactory,
        IAuthorizationService authorizationService,
        IMapper mapper)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _userClaimsPrincipalFactory = userClaimsPrincipalFactory;
        _authorizationService = authorizationService;
        _mapper = mapper;
    }

    public async Task Logout()
    {
        await _signInManager.SignOutAsync();
    }

    public async Task<string?> GetUserNameAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);

        return user?.UserName;
    }

    public async Task<(Result Result, string UserId)> CreateUserAsync(UserDTOs user)
    {
        var id = _userManager.Users.Select(e => e.StaffCode).ToList();

        var userName = _userManager.Users.Select(e => e.UserName).ToList();

        //var httpContexts = _httpContextAccessor.HttpContext;

        //var location = httpContexts.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Locality)?.Value;

        var newUser = new ApplicationUser
        {
            FirstName = user.FirstName,
            LastName = user.LastName,
            IsDelete = false,
            DateOfBirth = user.DateOfBirth,
            JoinDate = user.JoinDate,
            StaffCode = id.GenerateNewStaffCode(),
            Id = Guid.NewGuid().ToString(),
            UserName = userName.GenerateUsername(user.FirstName, user.LastName),
            Location = "HCM"
        };
        var password = newUser.UserName.GeneratePassword(user.DateOfBirth);

        var result = await _userManager.CreateAsync(newUser, password);
        if (result.Succeeded)
        {
            await _userManager.AddToRoleAsync(newUser, user.Role);
        }
        return (result.ToApplicationResult(), newUser.StaffCode);
    }

    public async Task<UserDto> GetUserWithRoleAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        
        Guard.Against.NotFound(userId, user);
        
        var roles = await _userManager.GetRolesAsync(user);
        var role = roles.FirstOrDefault() ?? string.Empty;
        
        var userDto = _mapper.Map<UserDto>(user);
        userDto.Type = role;
        
        return userDto;
    }

    public async Task<Result> UpdateUserAsync(UserDto userDto)
    {
        var user = await _userManager.FindByIdAsync(userDto.Id);
        
        Guard.Against.NotFound(userDto.Id, user);
        
        _mapper.Map(userDto, user);
        
        var result = await _userManager.UpdateAsync(user);
        
        return result.ToApplicationResult();
    }

    public async Task<Result> UpdateUserToRoleAsync(string userId, string currentRole, string newRole)
    {
        var user = await _userManager.FindByIdAsync(userId);
        
        Guard.Against.NotFound(userId, user);
        
        await _userManager.RemoveFromRoleAsync(user, currentRole);
        
        var result = await _userManager.AddToRoleAsync(user, newRole);

        return result.ToApplicationResult();
    }

    public async Task<bool> IsInRoleAsync(string userId, string role)
    {
        var user = await _userManager.FindByIdAsync(userId);

        return user != null && await _userManager.IsInRoleAsync(user, role);
    }

    public async Task<bool> AuthorizeAsync(string userId, string policyName)
    {
        var user = await _userManager.FindByIdAsync(userId);

        if (user == null)
        {
            return false;
        }

        var principal = await _userClaimsPrincipalFactory.CreateAsync(user);

        var result = await _authorizationService.AuthorizeAsync(principal, policyName);

        return result.Succeeded;
    }

    public async Task<Result> DeleteUserAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);

        return user != null ? await DeleteUserAsync(user) : Result.Success();
    }

    public async Task<Result> DeleteUserAsync(ApplicationUser user)
    {
        var result = await _userManager.DeleteAsync(user);

        return result.ToApplicationResult();
    }

    private async Task EnrichClaims(ApplicationUser user)
    {
        await _userManager.AddClaimAsync(user,
                            new Claim(ClaimTypes.GivenName, user.UserName ?? string.Empty));
    }

    public async Task<bool> IsUserDisabledAsync(string email)
    {
       var user = await _userManager.FindByEmailAsync(email);

        return  user != null && user.IsDelete;
    }
}
