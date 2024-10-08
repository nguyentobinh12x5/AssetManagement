using System.Security.Claims;

using AssetManagement.Application.Auth.Commands.Login;
using AssetManagement.Application.Auth.Queries.GetCurrentUserInfo;
using AssetManagement.Application.Common.Exceptions;
using AssetManagement.Application.Common.Extensions;
using AssetManagement.Application.Common.Interfaces;
using AssetManagement.Application.Common.Models;
using AssetManagement.Application.Users.Commands.Create;
using AssetManagement.Application.Users.Queries.GetUser;
using AssetManagement.Application.Users.Queries.GetUsers;
using AssetManagement.Infrastructure.Data;

using AutoMapper;


using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace AssetManagement.Infrastructure.Identity;

public class IdentityService : IIdentityService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager _signInManager;
    private readonly ApplicationDbContext _applicationDbContext;
    private readonly IUserClaimsPrincipalFactory<ApplicationUser> _userClaimsPrincipalFactory;
    private readonly IAuthorizationService _authorizationService;
    private readonly IMapper _mapper;
    private readonly IUser _currentUser;

    public IdentityService(
        UserManager<ApplicationUser> userManager,
        SignInManager signInManager,
        IUserClaimsPrincipalFactory<ApplicationUser> userClaimsPrincipalFactory,
        IAuthorizationService authorizationService,
        ApplicationDbContext applicationDbContext,
        IMapper mapper,
        IUser currentUser)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _userClaimsPrincipalFactory = userClaimsPrincipalFactory;
        _authorizationService = authorizationService;
        _mapper = mapper;
        _currentUser = currentUser;
        _applicationDbContext = applicationDbContext;

        _currentUser = currentUser;
    }

    public async Task Logout()
    {
        await _signInManager.SignOutAsync();
    }

    public async Task<Result> Login(LoginCommand request)
    {

        var useCookieScheme = (request.UseCookies == true) || (request.UseSessionCookies == true);
        var isPersistent = (request.UseCookies == true) && (request.UseSessionCookies != true);
        _signInManager.AuthenticationScheme = useCookieScheme ? IdentityConstants.ApplicationScheme : IdentityConstants.BearerScheme;

        // Login user
        var result = await _signInManager.PasswordSignInAsync(request.Email, request.Password, isPersistent, lockoutOnFailure: true);

        if (result.RequiresTwoFactor)
        {
            result = await TwoFactorSignIn(request, isPersistent, result);
        }

        if (!result.Succeeded)
        {
            if (result.IsLockedOut)
            {
                throw new InvalidAuthenticationException("Your account is disabled. Please contact with IT Team");
            }

            throw new InvalidAuthenticationException("Username or password is incorrect. Please try again");
        }

        // The signInManager already produced the needed response in the form of a cookie or bearer token.
        var success = IdentityResult.Success;
        return success.ToApplicationResult();
    }

    private async Task<SignInResult> TwoFactorSignIn(LoginCommand request, bool isPersistent, SignInResult result)
    {
        if (!string.IsNullOrEmpty(request.TwoFactorCode))
        {
            result = await _signInManager.TwoFactorAuthenticatorSignInAsync(request.TwoFactorCode, isPersistent, rememberClient: isPersistent);
        }
        else if (!string.IsNullOrEmpty(request.TwoFactorRecoveryCode))
        {
            result = await _signInManager.TwoFactorRecoveryCodeSignInAsync(request.TwoFactorRecoveryCode);
        }

        return result;
    }

    public async Task<string?> GetUserNameAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);

        return user?.UserName;
    }

    public async Task<(Result Result, string Id)> CreateUserAsync(CreateUserDto createUser)
    {
        Guard.Against.NullOrEmpty(_currentUser.Location);
        var id = _userManager.Users.Select(e => e.StaffCode).ToList();

        var userName = _userManager.Users.IgnoreQueryFilters().Select(e => e.UserName).ToList();


        var newUser = new ApplicationUser
        {
            FirstName = createUser.FirstName,
            LastName = createUser.LastName,
            IsDelete = false,
            DateOfBirth = createUser.DateOfBirth,
            JoinDate = createUser.JoinDate,
            Gender = createUser.Gender,
            StaffCode = id.GenerateNewStaffCode(),
            Id = Guid.NewGuid().ToString(),
            UserName = userName.GenerateUsername(createUser.FirstName, createUser.LastName),
            Location = _currentUser.Location
        };
        var password = newUser.UserName.GeneratePassword(createUser.DateOfBirth);

        var result = await _userManager.CreateAsync(newUser, password);
        if (result.Succeeded)
        {
            await _userManager.AddToRoleAsync(newUser, createUser.Role);
        }
        return (result.ToApplicationResult(), newUser.Id);
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

        Guard.Against.NotFound(userId, user);

        return await DeleteUserAsync(user);
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

    public async Task<PaginatedList<UserBriefDto>> GetUserBriefsAsync(GetUsersQuery query)
    {
        var users = await InitialGetUserBriefAsync(query.SearchTerm ?? "", _currentUser.Location ?? "", query.SortColumnName, query.SortColumnDirection);

        var userBriefDtos = await GetUserBriefDtosWithRoleAsync(users, query.Types.IsNullOrEmpty() ? "All" : query.Types);

        if (query.SortColumnName.Equals("Type", StringComparison.OrdinalIgnoreCase))
            userBriefDtos = FinalGetUserBriefAsync(userBriefDtos, query.SortColumnDirection);

        return new PaginatedList<UserBriefDto>(
            userBriefDtos
                .Skip((query.PageNumber - 1) * query.PageSize)
                .Take(query.PageSize)
                .ToList(),
            userBriefDtos.Count,
            query.PageNumber,
            query.PageSize
        );
    }

    // Handles the case where User's role was chosen to be sorted
    // in which case won't be able to since it's not of User's prop
    private async Task<List<UserBriefDto>> GetUserBriefDtosWithRoleAsync(List<ApplicationUser> users, string types)
    {
        var userTypes = types.Split(",");
        var userBriefDtos = new List<UserBriefDto>();

        var userIds = users.Select(u => u.Id).ToList();

        var userRoles = await _applicationDbContext.UserRoles
            .Where(ur => userIds.Contains(ur.UserId))
            .Join(_applicationDbContext.Roles,
                ur => ur.RoleId,
                r => r.Id,
                (ur, r) => new { ur.UserId, RoleName = r.Name })
            .ToListAsync();

        foreach (var user in users)
        {
            var userBriefDto = _mapper.Map<UserBriefDto>(user);

            var userRole = userRoles.Find(ur => ur.UserId == user.Id);

            userBriefDto.Type = userRole?.RoleName ?? "Default";

            userBriefDto.FullName = $"{user.FirstName} {user.LastName} ";

            userBriefDtos.Add(userBriefDto);
        }

        if (!userTypes.Contains("all", StringComparer.OrdinalIgnoreCase))
            userBriefDtos = userBriefDtos
                .Where(u => userTypes.Contains(u.Type, StringComparer.OrdinalIgnoreCase))
                .ToList();

        return userBriefDtos;
    }

    private async Task<List<ApplicationUser>> InitialGetUserBriefAsync(string SearchTerm, string location, string columnName, string columnDirection)
    {
        var searchTermLower = SearchTerm;
        if (!columnName.Equals("Type", StringComparison.OrdinalIgnoreCase))
        {
            return await _userManager.Users
                .Where(u => u.Location == location &&
                    (EF.Functions.Like((u.FirstName + " " + u.LastName).ToLower(), $"%{searchTermLower}%") ||
                    u.StaffCode.ToLower().Contains(searchTermLower)))
                .OrderByDynamic(columnName, columnDirection)
                .ToListAsync();
        }
        else
        {
            return await _userManager.Users
                .Where(u => u.Location == location &&
                    (EF.Functions.Like((u.FirstName + " " + u.LastName).ToLower(), $"%{searchTermLower}%") ||
                    u.StaffCode.ToLower().Contains(searchTermLower)))
                .OrderByDynamic("StaffCode", columnDirection)
                .ToListAsync();
        }
    }

    public async Task<bool> CheckCurrentPassword(string currentPassword)
    {
        var userId = _currentUser.Id!;

        var user = await _userManager.FindByIdAsync(userId);
        Guard.Against.NotFound(userId, user);

        var isCurrentPasswordValid = await _userManager.CheckPasswordAsync(user, currentPassword);
        if (!isCurrentPasswordValid)
        {
            throw new IncorrectPasswordException();
        }

        return true;
    }

    public async Task<Result> ChangePasswordAsync(string currentPassword, string newPassword)
    {
        var userId = _currentUser.Id!;

        var user = await _userManager.FindByIdAsync(userId);
        Guard.Against.NotFound(userId, user);

        var result = await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);

        if (!result.Succeeded)
        {
            var errors = result.Errors.Select(e => e.Description).ToList();
            return Result.Failure(errors);
        }

        return Result.Success();
    }

    public async Task<Result> ChangePasswordFirstTimeAsync(string newPassword)
    {
        var userId = _currentUser.Id;
        Guard.Against.NullOrWhiteSpace(userId);

        var user = await _userManager.FindByIdAsync(userId);
        Guard.Against.NotFound(userId, user);

        var token = await _userManager.GeneratePasswordResetTokenAsync(user);
        var result = await _userManager.ResetPasswordAsync(user, token, newPassword);

        if (!result.Succeeded)
        {
            var errors = result.Errors.Select(e => e.Description).ToList();
            return Result.Failure(errors);
        }

        // Update the MustChangePassword field
        user.MustChangePassword = false;
        var updateResult = await _userManager.UpdateAsync(user);

        if (!updateResult.Succeeded)
        {
            var errors = updateResult.Errors.Select(e => e.Description).ToList();
            return Result.Failure(errors);
        }

        return Result.Success();
    }

    public async Task<bool> IsSameOldPassword(string newPassword)
    {
        var userId = _currentUser.Id;

        Guard.Against.NullOrWhiteSpace(userId);
        var user = await _userManager.FindByIdAsync(userId);
        Guard.Against.NotFound(userId, user);

        // Check if the new password is the same as the current password
        var isCurrentPassword = await _userManager.CheckPasswordAsync(user, newPassword);
        if (isCurrentPassword)
        {
            return true;
        }

        return false;
    }

    private List<UserBriefDto> FinalGetUserBriefAsync(List<UserBriefDto> userBriefDto, string orderDirection)
    {
        return orderDirection.Equals("Descending", StringComparison.OrdinalIgnoreCase) ?
            userBriefDto.OrderByDescending(u => u.Type).ToList() :
            userBriefDto.OrderBy(u => u.Type).ToList();

    }

    public async Task<bool> IsUserDisabledAsync(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);

        return user != null && user.IsDelete;
    }

    public async Task<UserInfoDto> GetCurrentUserInfo(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        Guard.Against.NotFound(userId, user);

        var roles = await _userManager.GetRolesAsync(user);
        return new UserInfoDto
        {
            Username = user.UserName!,
            Roles = roles,
            MustChangePassword = user.MustChangePassword,
        };
    }
    public async Task<List<string?>> GetUserTypes()
    {
        return await _applicationDbContext.Roles
            .Select(r => r.Name)
            .ToListAsync();
    }
}