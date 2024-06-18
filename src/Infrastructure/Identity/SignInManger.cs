using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace AssetManagement.Infrastructure.Identity;

public class SignInManager : SignInManager<ApplicationUser>
{
    public SignInManager(UserManager<ApplicationUser> userManager, IHttpContextAccessor contextAccessor, IUserClaimsPrincipalFactory<ApplicationUser> claimsFactory, IOptions<IdentityOptions> optionsAccessor, ILogger<SignInManager<ApplicationUser>> logger, IAuthenticationSchemeProvider schemes, IUserConfirmation<ApplicationUser> confirmation) : base(userManager, contextAccessor, claimsFactory, optionsAccessor, logger, schemes, confirmation)
    {
    }

    public override async Task<SignInResult> PasswordSignInAsync(string userName, string password, bool isPersistent, bool lockoutOnFailure)
    {
        var user = await UserManager.FindByNameAsync(userName);

        if (user == null)
        {
            return SignInResult.Failed; // Or return SignInResult.NotFound if preferred
        }

        if (user.IsDelete)
        {
            return SignInResult.LockedOut; // Or another custom result for disabled users
        }

        // Continue with the regular password sign-in
        return await base.PasswordSignInAsync(userName, password, isPersistent, lockoutOnFailure);
    }
}