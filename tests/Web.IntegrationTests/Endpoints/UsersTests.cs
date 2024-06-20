using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

using AssetManagement.Application.Common.Models;
using AssetManagement.Application.Users.Queries.GetUsers;
using AssetManagement.Application.Users.Queries.GetUsersByType;

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
        public async Task GetUserByType_ShouldReturnUsers_WhenTypeIsValid()
        {
            // Arrange
            await UsersDataHelper.CreateSampleData(_factory);

            var pageNumber = 1;
            var pageSize = 5;
            var sortColumnName = "id";
            var sortColumnDirection = "Descending";
            var type = "Administrator";

            var url = $"/api/Users/type?PageNumber={pageNumber}&PageSize={pageSize}&SortColumnName={sortColumnName}&SortColumnDirection={sortColumnDirection}&Type={type}";

            // Act
            var response = await _httpClient.GetAsync(url);

            

            var responseContent = await response.Content.ReadAsStringAsync();
            Console.WriteLine("Response Content: " + responseContent);

            // Assert
            var users = await response.Content.ReadFromJsonAsync<PaginatedList<UserBriefDto>>();

            Assert.NotNull(users);
            Assert.NotEmpty(users.Items);
            Assert.True(users.Items.All(u => u.Type == type));
        }

        [Fact]
        public async Task GetUserByType_ShouldReturnEmpty_WhenTypeIsInvalid()
        {
            // Arrange
            await UsersDataHelper.CreateSampleData(_factory);

            var pageNumber = 1;
            var pageSize = 5;
            var sortColumnName = "StaffCode";
            var sortColumnDirection = "Ascending";
            var type = "InvalidType";

            var url = $"/api/Users/type?PageNumber={pageNumber}&PageSize={pageSize}&SortColumnName={sortColumnName}&SortColumnDirection={sortColumnDirection}&Type={type}";

            // Act
            var response = await _httpClient.GetAsync(url);

            var users = await response.Content.ReadFromJsonAsync<PaginatedList<UserBriefDto>>();

            // Assert
            Assert.NotNull(users);
            Assert.Empty(users.Items);
        }
    }
}
