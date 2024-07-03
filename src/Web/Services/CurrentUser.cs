using System.Security.Claims;

using AssetManagement.Application.Common.Interfaces;

namespace AssetManagement.Web.Services;

public class CurrentUser : IUser
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUser(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public string? Id => _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
    public string? UserName => _httpContextAccessor.HttpContext?.User?.FindFirstValue("UserName");

    public string? Location => _httpContextAccessor.HttpContext?.User?.FindFirstValue("Location");
}