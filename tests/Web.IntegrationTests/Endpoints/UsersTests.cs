using System.Net.Http.Json;

using AssetManagement.Application.Common.Models;
using AssetManagement.Application.Users.Queries.GetUsers;
using AssetManagement.Application.Users.Queries.GetUsersBySearch;

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
