using System.Net;
using System.Net.Http.Json;

using AssetManagement.Application.Common.Models;
using AssetManagement.Application.Users.Queries.GetUsers;
using AssetManagement.Application.Users.Queries.GetUsersBySearch;
using AssetManagement.Infrastructure.Data;
using AssetManagement.Infrastructure.Identity;

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

using Web.IntegrationTests.Extensions;
using Web.IntegrationTests.Helpers;

using Xunit;



using Assert = Xunit.Assert;


namespace Web.IntegrationTests.Endpoints
{
    [Collection("Sequential")]
    public class UsersTests : IClassFixture<TestWebApplicationFactory<Program>>
    {
        private readonly TestWebApplicationFactory<Program> _factory;
        private readonly HttpClient _httpClient;

        public UsersTests(TestWebApplicationFactory<Program> factory)
        {
            _factory = factory;
            _httpClient = _factory.GetApplicationHttpClient();
        }

        [Fact]
        public async Task DeleteUser_ShouldRemoveUser_WhenUserExists()
        {
            // Arrange
            await UsersDataHelper.CreateSampleData(_factory);

            using var scope = _factory.Services.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            var user = await userManager.FindByEmailAsync("user1@test.com");
            Assert.NotNull(user);

            // Act
            var response = await _httpClient.DeleteAsync($"/api/Users/{user!.Id}");

            // Assert
            var usersResponse = await _httpClient.GetAsync("/api/Users?Location=HCM&PageNumber=1&PageSize=5&SortColumnName=StaffCode&SortColumnDirection=Ascending");
            var users = await usersResponse.Content.ReadFromJsonAsync<PaginatedList<UserBriefDto>>();

            Assert.NotNull(users);
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);

            Assert.DoesNotContain(users.Items, u => u.Id == user.Id);
        }

        [Fact]
        public async Task DeleteUser_ShouldReturnNotFound_WhenUserDoesNotExist()
        {
            // Arrange
            using var scope = _factory.Services.CreateScope();

            // Act
            var response = await _httpClient.DeleteAsync($"/api/Users/{Guid.NewGuid()}");

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}