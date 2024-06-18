using AssetManagement.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;


namespace Web.IntegrationTests.Mocks;

public class MockUserManager : UserManager<ApplicationUser>
{
    public MockUserManager(
        IUserStore<ApplicationUser> store,
        IOptions<IdentityOptions> optionsAccessor,
        IPasswordHasher<ApplicationUser> passwordHasher,
        IEnumerable<IUserValidator<ApplicationUser>> userValidators,
        IEnumerable<IPasswordValidator<ApplicationUser>> passwordValidators,
        ILookupNormalizer keyNormalizer,
        IdentityErrorDescriber errors,
        IServiceProvider services,
        ILogger<UserManager<ApplicationUser>> logger) : base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
    {
    }

    // public override Task<ApplicationUser> FindByNameAsync(string userName)
    // {
    //     if (userName == "user@example.com")
    //     {
    //         return Task.FromResult(new ApplicationUser { UserName = userName, Email = userName });
    //     }
    //     return Task.FromResult<ApplicationUser>(null);
    // }

    public override Task<bool> CheckPasswordAsync(ApplicationUser user, string password)
    {
        if (user.UserName == "user@example.com" && password == "Password123!")
        {
            return Task.FromResult(true);
        }
        return Task.FromResult(false);
    }

    public override Task<IdentityResult> CreateAsync(ApplicationUser user, string password)
    {
        return Task.FromResult(IdentityResult.Success);
    }

    // Add other methods as necessary
}
