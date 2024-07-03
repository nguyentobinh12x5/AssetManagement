using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

using AssetManagement.Application.Common.Models;
using AssetManagement.Application.Users.Commands.Create;
using AssetManagement.Application.Users.Queries.GetUsers;
using AssetManagement.Domain.Enums;
using AssetManagement.Infrastructure.Data;
using AssetManagement.Infrastructure.Identity;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("TestScheme");

        }

        [Fact]
        public async Task GetUsers_ShouldGetUserPaginatedList_WhenUserExists()
        {
            // Arrange
            await UsersDataHelper.CreateSampleData(_factory);

            using var scope = _factory.Services.CreateScope();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            // Act
            var response = await _httpClient.GetAsync("/api/Users?PageNumber=1&PageSize=5&SortColumnName=StaffCode&SortColumnDirection=Ascending&Types=");

            // Assert
            var users = await response.Content.ReadFromJsonAsync<PaginatedList<UserBriefDto>>();

            Assert.NotNull(users);
            Assert.NotEmpty(users.Items);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(userManager.Users.Where(u => u.Location.Equals(_factory.TestUserLocation)).Count(), users.TotalCount);
            Assert.Equal(1, users.PageNumber);
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
            var usersResponse = await _httpClient.GetAsync("/api/Users?PageNumber=1&PageSize=5&SortColumnName=StaffCode&SortColumnDirection=Ascending&Types=");
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
        [Fact]
        public async Task Create_User_Endpoint_Returns_Success()
        {
            // Arrange
            var command = new CreateUserCommand
            {
                FirstName = "John",
                LastName = "Doe",
                Location = "New York",
                DateOfBirth = new DateTime(1990, 1, 1),
                Gender = Gender.Male,
                JoinDate = DateTime.UtcNow,
                Type = "Staff"
            };

            var content = new StringContent(JsonSerializer.Serialize(command), Encoding.UTF8, "application/json");

            // Act
            var response = await _httpClient.PostAsync("/api/users", content);

            // Assert
            Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}