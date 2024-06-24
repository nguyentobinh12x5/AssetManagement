using System.Net.Http.Json;
using System.Threading.Tasks;

using AssetManagement.Application.Common.Models;
using AssetManagement.Application.Users.Queries.GetUsers;
using AssetManagement.Infrastructure.Data;
using AssetManagement.Infrastructure.Identity;

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

using Web.IntegrationTests.Extensions;
using Web.IntegrationTests.Helpers;

using Xunit;

using Assert = Xunit.Assert;

using Xunit;

using Assert = Xunit.Assert;

namespace Web.IntegrationTests.Endpoints
{
    [Collection("Sequential")]
    public class UsersTests : IClassFixture<TestWebApplicationFactory<Program>>
    {
        private readonly TestWebApplicationFactory<Program> _factory;
        private readonly HttpClient _httpClient;
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
            var sortColumnName = "StaffCode";
            var sortColumnDirection = "Descending";
            var type = "Administrator";

            var url = $"/api/Users/type?PageNumber={pageNumber}&PageSize={pageSize}&SortColumnName={sortColumnName}&SortColumnDirection={sortColumnDirection}&Types={type}";

            // Act
            var response = await _httpClient.GetAsync(url);

            var responseContent = await response.Content.ReadAsStringAsync();
            Console.WriteLine("Response Content: " + responseContent);

            // Assert
            var users = await response.Content.ReadFromJsonAsync<PaginatedList<UserBriefDto>>();

            Assert.NotNull(users);
            Assert.NotEmpty(users.Items);
            Assert.All(users.Items, u => Assert.Equal(type, u.Type));
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

            var url = $"/api/Users/type?PageNumber={pageNumber}&PageSize={pageSize}&SortColumnName={sortColumnName}&SortColumnDirection={sortColumnDirection}&Types={type}";

            // Act
            var response = await _httpClient.GetAsync(url);

            var responseContent = await response.Content.ReadAsStringAsync();
            Console.WriteLine("Response Content: " + responseContent);

            // Assert
            var users = await response.Content.ReadFromJsonAsync<PaginatedList<UserBriefDto>>();

            Assert.NotNull(users);
            Assert.Empty(users.Items);
        }
        [Fact]
        public async Task SearchUsers_ShouldReturnUsers_WhenSearchTermIsValid()
        {
            // Arrange
            await UsersDataHelper.CreateSampleData(_factory);

            var query = new GetUsersBySearchQuery
            {
                SearchTerm = "user1",
                PageNumber = 1,
                PageSize = 5,
                SortColumnName = "StaffCode",
                SortColumnDirection = "Ascending"
            };

            // Act
            var response = await _httpClient.GetAsync($"/api/Users/Search?SearchTerm={query.SearchTerm}&PageNumber={query.PageNumber}&PageSize={query.PageSize}&SortColumnName={query.SortColumnName}&SortColumnDirection={query.SortColumnDirection}");

            var responseContent = await response.Content.ReadAsStringAsync();
            Console.WriteLine("Response Content: " + responseContent);

            // Assert
            var users = await response.Content.ReadFromJsonAsync<PaginatedList<UserBriefDto>>();

            Assert.NotNull(users);
            Assert.All(users.Items, user => Assert.Contains(query.SearchTerm, user.UserName, StringComparison.OrdinalIgnoreCase));
        }
        [Fact]
        public async Task SearchUsers_ShouldReturnEmpty_WhenSearchTermIsInvalid()
        {
            // Arrange
            await UsersDataHelper.CreateSampleData(_factory);

            var query = new GetUsersBySearchQuery
            {
                SearchTerm = "nonexistinguser",
                PageNumber = 1,
                PageSize = 5,
                SortColumnName = "StaffCode",
                SortColumnDirection = "Ascending"
            };

            // Act
            var response = await _httpClient.GetAsync($"/api/Users/Search?SearchTerm={query.SearchTerm}&PageNumber={query.PageNumber}&PageSize={query.PageSize}&SortColumnName={query.SortColumnName}&SortColumnDirection={query.SortColumnDirection}");

            var responseContent = await response.Content.ReadAsStringAsync();
            Console.WriteLine("Response Content: " + responseContent);

            // Assert
            var users = await response.Content.ReadFromJsonAsync<PaginatedList<UserBriefDto>>();
            Assert.NotNull(users);
            Assert.Empty(users.Items);
        }
    }
}