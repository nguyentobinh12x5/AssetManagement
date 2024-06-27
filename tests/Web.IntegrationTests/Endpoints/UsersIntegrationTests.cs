using System.Net;
using System.Text;
using System.Text.Json;

using AssetManagement.Application.Users.Commands.Create;
using AssetManagement.Domain.Enums;

using Web.IntegrationTests.Extensions;
using Web.IntegrationTests.Helpers;

using Xunit;

namespace Web.IntegrationTests.Endpoints
{

    [Collection("Sequential")]
    public class UsersIntegrationTests : IClassFixture<TestWebApplicationFactory<Program>>
    {
        private readonly TestWebApplicationFactory<Program> _factory;
        private readonly HttpClient _httpClient;

        public UsersIntegrationTests(TestWebApplicationFactory<Program> factory)
        {
            _factory = factory;
            _httpClient = _factory.GetApplicationHttpClient();
        }

        [Fact(Skip = "For smoke test")]
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