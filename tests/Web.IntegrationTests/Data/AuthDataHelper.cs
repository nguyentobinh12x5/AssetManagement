using AssetManagement.Domain.Constants;
using AssetManagement.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Web.IntegrationTests.Helpers;

namespace Web.IntegrationTests.Data;

public static class AuthDataHelper
{
    private static ApplicationUser DisabledUser = new ApplicationUser
    {
        UserName = "disabledAdmin@localhost",
        Email = "disabledAdmin@localhost",
        FirstName = "Disabled",
        LastName = "Admin",
        StaffCode = "AD0001",
        Location = "HCM",
        IsDelete = true
    };
    private static string Password = "Administrator1!";

    public static async Task CreateSampleData(
        TestWebApplicationFactory<Program> factory
    )
    {
        using (var scope = factory.Services.CreateScope())
        {
            var roleManager = scope.ServiceProvider.GetService<RoleManager<IdentityRole>>();
            var userManager = scope.ServiceProvider.GetService<UserManager<ApplicationUser>>();


            if (roleManager != null && userManager != null)
            {
                // Default roles
                var administratorRole = new IdentityRole(Roles.Administrator);

                if (roleManager.Roles.All(r => r.Name != administratorRole.Name))
                {
                    await roleManager.CreateAsync(administratorRole);
                }

                if (userManager.Users.All(u => u.UserName != DisabledUser.UserName))
                {
                    await userManager.CreateAsync(DisabledUser, Password);
                    if (!string.IsNullOrWhiteSpace(administratorRole.Name))
                    {
                        await userManager.AddToRolesAsync(DisabledUser, new[] { administratorRole.Name });
                    }
                }
            }
        }
    }
}