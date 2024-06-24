using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;

using AssetManagement.Application.Assets.Queries.GetDetailedAssets;

using Web.IntegrationTests.Data;
using Web.IntegrationTests.Extensions;
using Web.IntegrationTests.Helpers;

using Xunit;

using Assert = Xunit.Assert;

namespace Web.IntegrationTests.Endpoints
{
    [Collection("Sequential")]
    public class AssetsTests : IClassFixture<TestWebApplicationFactory<Program>>
    {
        private readonly TestWebApplicationFactory<Program> _factory;
        private readonly HttpClient _httpClient;

        public AssetsTests(TestWebApplicationFactory<Program> factory)
        {
            _factory = factory;
            _httpClient = _factory.GetApplicationHttpClient();
        }

        [Fact]
        public async Task GetAsset_ValidId_ReturnsAssetDto()
        {
            // Arrange
            await AssetDataHelper.CreateSampleData(_factory);
            var assetId = 1;

            // Act
            var assetDto = await _httpClient.GetFromJsonAsync<AssetDto>($"/Assets/{assetId}");

            // Assert
            Assert.NotNull(assetDto);
            Assert.Equal(assetId, assetDto.Id);
        }

        [Fact]
        public async Task GetAsset_InvalidId_ReturnsNotFound()
        {
            // Arrange
            var invalidAssetId = 999;

            // Act
            var response = await _httpClient.GetAsync($"/Assets/{invalidAssetId}");

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}