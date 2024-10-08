using AssetManagement.Infrastructure.Data;
using AssetManagement.Infrastructure.Identity;

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Web.IntegrationTests.Helpers
{
    public static class UsersDataHelper
    {
        public static readonly string TestUserId = Guid.NewGuid().ToString();
        public static readonly string TestLocation = "HCM";
        public static readonly string TestUserName = "user1@test.com";

        private static readonly List<ApplicationUser> UsersList = new()
        {
            new ApplicationUser
            {
                Id = TestUserId,
                UserName = TestUserName,
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
                Location = TestLocation,
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
                var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                var userNames = UsersList.Select(u => u.UserName);
                if (dbContext != null && !dbContext.Users.Any(u => userNames.Contains(u.UserName)))
                {
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
                            }
                        }
                    }
                }
            }
        }
    }
}