using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

using AssetManagement.Application.Assets.Commands.Create;
using AssetManagement.Infrastructure.Data;

using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;

using Xunit;

namespace Web.IntegrationTests.Endpoints
{
    public class AssetManagementIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        public AssetManagementIntegrationTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        [Fact(Skip = "Not use global setting")]
        public async Task AddAsset_ValidCommand_ReturnsSuccess()
        {
            // Arrange
            var client = _factory.CreateClient();

            var command = new CreateNewAssetCommand
            {
                Name = "Test Asset",
                Category = "Laptop",
                Specification = "Test Specification",
                InstallDate = DateTime.UtcNow,
                State = "Available",
                Location = "Test Location"
            };

            var content = new StringContent(JsonSerializer.Serialize(command), Encoding.UTF8, "application/json");

            // Act
            var response = await client.PostAsync("/api/assets", content);

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}