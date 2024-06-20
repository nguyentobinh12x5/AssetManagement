using AssetManagement.Infrastructure.Data;
using AssetManagement.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Web.IntegrationTests.Helpers
{
    public static class UsersDataHelper
    {
        private static List<ApplicationUser> UsersList = new()
        {
            new ApplicationUser
            {
                Id = Guid.NewGuid().ToString(),
                UserName = "user1@test.com",
                Email = "user1@test.com",
                FirstName = "John",
                LastName = "Doe",
                Location = "Location1",
                StaffCode = "SC001",
                DateOfBirth = DateTime.UtcNow.AddYears(-30),
                JoinDate = DateTime.UtcNow.AddYears(-1),
                Gender = AssetManagement.Domain.Enums.Gender.Male,
                MustChangePassword = true,
                IsDelete = false
            },
            new ApplicationUser
            {
                Id = Guid.NewGuid().ToString(),
                UserName = "user2@test.com",
                Email = "user2@test.com",
                FirstName = "Jane",
                LastName = "Smith",
                Location = "Location2",
                StaffCode = "SC002",
                DateOfBirth = DateTime.UtcNow.AddYears(-25),
                JoinDate = DateTime.UtcNow.AddMonths(-6),
                Gender = AssetManagement.Domain.Enums.Gender.Female,
                MustChangePassword = true,
                IsDelete = false
            }
        };

        public static async Task CreateSampleData(TestWebApplicationFactory<Program> factory)
        {
            using (var scope = factory.Services.CreateScope())
            {
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                var roles = new[] { "Administrator", "Staff" };

                foreach (var role in roles)
                {
                    if (!await roleManager.RoleExistsAsync(role))
                    {
                        await roleManager.CreateAsync(new IdentityRole(role));
                    }
                }

                foreach (var user in UsersList)
                {
                    if (!string.IsNullOrEmpty(user.Email))
                    {
                        var existingUser = await userManager.FindByEmailAsync(user.Email);
                        if (existingUser == null)
                        {
                            var result = await userManager.CreateAsync(user, "Password123!");
                            if (!result.Succeeded)
                            {
                                throw new Exception($"Failed to create user {user.Email}: {string.Join(", ", result.Errors.Select(e => e.Description))}");
                            }

                            var role = user.StaffCode == "SC001" ? "Administrator" : "Staff";
                            await userManager.AddToRoleAsync(user, role);
                        }
                    }
                }
            }
        }
    }
}
