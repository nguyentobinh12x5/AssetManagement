using System.Collections.ObjectModel;
using System.Runtime.InteropServices;
using System.Security.Claims;
using AssetManagement.Application.Common.Interfaces;
using AssetManagement.Application.Common.Mappings;
using AssetManagement.Application.Common.Models;
using AssetManagement.Application.Users.Queries.GetUsers;
using AssetManagement.Infrastructure.Data;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AssetManagement.Infrastructure.Identity;

public class IdentityService : IIdentityService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly ApplicationDbContext _applicationDbContext;
    private readonly IUserClaimsPrincipalFactory<ApplicationUser> _userClaimsPrincipalFactory;
    private readonly IAuthorizationService _authorizationService;
    private readonly IMapper _mapper;

    public IdentityService(
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        IUserClaimsPrincipalFactory<ApplicationUser> userClaimsPrincipalFactory,
        IAuthorizationService authorizationService,
        ApplicationDbContext applicationDbContext,
        IMapper mapper)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _userClaimsPrincipalFactory = userClaimsPrincipalFactory;
        _authorizationService = authorizationService;
        _mapper = mapper;
        _applicationDbContext = applicationDbContext;

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

    public async Task<(Result Result, string UserId)> CreateUserAsync(string userName, string password)
    {
        var user = new ApplicationUser
        {
            UserName = userName,
            Email = userName
        };

        var result = await _userManager.CreateAsync(user, password);

        return (result.ToApplicationResult(), user.Id);
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
        var users = await InitialGetUserBriefAsync(query);

        //var test = users.FirstOrDefault()?.UserRoles?.FirstOrDefault()?.Role.Name;

        var userBriefDtos = await GetUserBriefDtosWithRoleAsync(users);
    
        if(query.SortColumnName.Equals("Type", StringComparison.OrdinalIgnoreCase))
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
    private async Task<List<UserBriefDto>> GetUserBriefDtosWithRoleAsync(List<ApplicationUser> users)
    {
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
            
            var userRole = userRoles.FirstOrDefault(ur => ur.UserId == user.Id);
            userBriefDto.Type = userRole?.RoleName;

            userBriefDto.FullName = $"{user.FirstName} {user.LastName} ";

            userBriefDtos.Add(userBriefDto);
        }

        return userBriefDtos;
    }

    private async Task<List<ApplicationUser>> InitialGetUserBriefAsync(GetUsersQuery query)
    {    
        if( !query.SortColumnName.Equals("Type", StringComparison.OrdinalIgnoreCase)){ 
            return await _userManager.Users                
                .OrderByDynamic(query.SortColumnName, query.SortColumnDirection)            
                .ToListAsync();
        }
        else{
            return await _userManager.Users
                .OrderByDynamic("StaffCode", query.SortColumnDirection)
                .ToListAsync();
        }
    }

    private List<UserBriefDto> FinalGetUserBriefAsync(List<UserBriefDto> userBriefDto, string orderDirection)
    {
        return orderDirection.Equals("Descending", StringComparison.OrdinalIgnoreCase) ? 
            userBriefDto.OrderByDescending( u => u.Type ).ToList():
            userBriefDto.OrderBy( u => u.Type ).ToList();
    }

    public async Task<bool> IsUserDisabledAsync(string email)
    {
       var user = await _userManager.FindByEmailAsync(email);

        return  user != null && user.IsDelete;
    }
}
