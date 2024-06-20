using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using AssetManagement.Application.Users.Commands.Create;
using AssetManagement.Domain.Enums;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace Web.IntegrationTests.Endpoints
{
    public class UsersIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        public UsersIntegrationTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task Create_User_Endpoint_Returns_Success()
        {
            // Arrange
            var client = _factory.CreateClient();
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
            var response = await client.PostAsync("/api/users", content);

            // Assert
            response.EnsureSuccessStatusCode(); 
            Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
