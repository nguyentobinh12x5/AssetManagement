using System.Security.Claims;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace AssetManagement.Infrastructure.Identity;

public class SignInManager : SignInManager<ApplicationUser>
{
    public SignInManager(
        UserManager<ApplicationUser> userManager,
        IHttpContextAccessor contextAccessor,
        IUserClaimsPrincipalFactory<ApplicationUser> claimsFactory,
        IOptions<IdentityOptions> optionsAccessor,
        ILogger<SignInManager<ApplicationUser>> logger,
        IAuthenticationSchemeProvider schemes,
        IUserConfirmation<ApplicationUser> confirmation) : base(userManager, contextAccessor, claimsFactory, optionsAccessor, logger, schemes, confirmation)
    {
    }

    public override async Task<SignInResult> PasswordSignInAsync(string userName, string password, bool isPersistent, bool lockoutOnFailure)
    {
        var user = await UserManager.FindByNameAsync(userName);

        if (user == null)
        {
            return SignInResult.Failed; // Or return SignInResult.NotFound if preferred
        }

        // Continue with the regular password sign-in
        var result = await PasswordSignInAsync(user, password, isPersistent, lockoutOnFailure);

        if (result.Succeeded)
        {
                var locationClaim = new Claim("Location", user.Location);
                await UserManager.AddClaimAsync(user, locationClaim);
        }
        return result;
    }

    public override async Task<SignInResult> PasswordSignInAsync(ApplicationUser user, string password, bool isPersistent, bool lockoutOnFailure)
    {
        ArgumentNullException.ThrowIfNull(user);

        var attempt = await CheckPasswordSignInAsync(user, password, lockoutOnFailure);
        if (attempt.Succeeded)
        {
            if (user.IsDelete)
            {
                return SignInResult.LockedOut; // Or another custom result for disabled users
            }
            return await SignInOrTwoFactorAsync(user, isPersistent);
        }

        return attempt;
    }
}