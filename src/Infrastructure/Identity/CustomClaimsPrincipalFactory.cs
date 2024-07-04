using System.Security.Claims;

using AssetManagement.Infrastructure.Identity;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

public class CustomClaimsPrincipalFactory : UserClaimsPrincipalFactory<ApplicationUser>
{
    public CustomClaimsPrincipalFactory(UserManager<ApplicationUser> userManager, IOptions<IdentityOptions> optionsAccessor) : base(userManager, optionsAccessor)
    {
    }


    protected override async Task<ClaimsIdentity> GenerateClaimsAsync(ApplicationUser user)
    {
        var identity = await base.GenerateClaimsAsync(user);
        identity.AddClaim(new Claim("UserName", user.UserName ?? string.Empty));
        identity.AddClaim(new Claim("Location", user.Location ?? string.Empty));

        return identity;
    }
}