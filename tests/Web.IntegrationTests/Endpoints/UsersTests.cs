using System.Net;
using System.Net.Http.Json;
using AssetManagement.Application.Users.Commands.UpdateUser;
using AssetManagement.Application.Users.Queries.GetUser;
using AssetManagement.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using Assert = Xunit.Assert;
using Web.IntegrationTests.Extensions;
using Web.IntegrationTests.Helpers;

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
        public async Task GetUserById_ShouldReturnUser_WhenUserExists()
        {
            // Arrange
            await UsersDataHelper.CreateSampleData(_factory);

            using var scope = _factory.Services.CreateScope();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            var user = await userManager.FindByEmailAsync("user1@test.com");
            Assert.NotNull(user);

            // Act
            var response = await _httpClient.GetAsync($"/api/Users/{user!.Id}");

            // Assert
            response.EnsureSuccessStatusCode(); 
            var userDto = await response.Content.ReadFromJsonAsync<UserDto>();

            Assert.NotNull(userDto);
            Assert.Equal(user.Id, userDto!.Id);
        }

        [Fact]
        public async Task GetUserById_ShouldReturnNotFound_WhenUserDoesNotExist()
        {
            // Arrange
            using var scope = _factory.Services.CreateScope();

            var userId = "user-Id";
            // Act
            var response = await _httpClient.GetAsync($"/api/Users/{userId}");

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task UpdateUserById_ShouldReturnUser_WhenUserExists()
        {
            // Arrange
            await UsersDataHelper.CreateSampleData(_factory);

            using var scope = _factory.Services.CreateScope();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            var user = await userManager.FindByEmailAsync("user1@test.com");
            Assert.NotNull(user);
            
            var updateUserRequest = new UpdateUserCommand()
            {
                Id = user.Id,
                FirstName = "UpdatedFirstName",
                LastName = "UpdatedLastName",
                DateOfBirth = DateTime.UtcNow,
                JoinDate = DateTime.UtcNow,
                Gender = AssetManagement.Domain.Enums.Gender.Female,
                Type = "Staff",
            };
            
            //Act
            await _httpClient.PutAsJsonAsync($"/api/Users/{user.Id}", updateUserRequest);
            
            //Assert
            var userDto = await _httpClient.GetFromJsonAsync<UserDto>($"/api/Users/{user.Id}");

            Assert.NotNull(userDto);
            Assert.Equal(updateUserRequest.FirstName, userDto!.FirstName);
            Assert.Equal(updateUserRequest.LastName, userDto!.LastName);
            Assert.Equal(updateUserRequest.DateOfBirth, userDto!.DateOfBirth);
            Assert.Equal(updateUserRequest.JoinDate, userDto!.JoinDate);
        }

        [Fact]
        public async Task UpdateUserById_ShouldReturnBadRequest_WhenIdDoesNotMatch()
        {
            // Arrange
            await UsersDataHelper.CreateSampleData(_factory);

            using var scope = _factory.Services.CreateScope();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            var user = await userManager.FindByEmailAsync("user1@test.com");
            Assert.NotNull(user);

            var updateUserRequest = new UpdateUserCommand()
            {
                Id = "InvalidId",
                FirstName = "UpdatedFirstName",
                LastName = "UpdatedLastName",
                DateOfBirth = DateTime.UtcNow,
                JoinDate = DateTime.UtcNow,
                Gender = AssetManagement.Domain.Enums.Gender.Female,
                Type = "Staff",
            };
            
            //Act
            var response = await _httpClient.PutAsJsonAsync($"/api/Users/{user.Id}", updateUserRequest);
            
            //Assert
            
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task UpdateUserById_ShouldReturnNotFound_WhenUserDoesNotExist()
        {
            // Arrange
            using var scope = _factory.Services.CreateScope();

            var userId = "user-id";
            var updateUserRequest = new UpdateUserCommand()
            {
                Id = userId,
                FirstName = "UpdatedFirstName",
                LastName = "UpdatedLastName",
                DateOfBirth = DateTime.UtcNow,
                JoinDate = DateTime.UtcNow,
                Gender = AssetManagement.Domain.Enums.Gender.Female,
                Type = "Staff",
            };
            //Act
            var response = await _httpClient.PutAsJsonAsync($"/api/Users/{userId}", updateUserRequest);

            //Asser 
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
